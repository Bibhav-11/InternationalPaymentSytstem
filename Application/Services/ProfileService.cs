using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Domain.Entities.Application;
using Infrastructure;

namespace Application.Services;

public class ProfileService: IProfileService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _uow;
    private readonly int _currentUserId;
    private readonly IExchangeRateService _exchangeRateService;

    public ProfileService(ICurrentUserService currentUserService, IUnitOfWork uow, IExchangeRateService exchangeRateService)
    {
        _currentUserService = currentUserService;
        _uow = uow;
        _exchangeRateService = exchangeRateService;
        _currentUserId = _currentUserService.UserId ?? 0;
    }


    public async Task<ProfileInfoViewModel> GetProfileInfo()
    {
        var dbProfile = await _uow.AsyncRepository<UserProfile>().GetSingleBySpec(x => x.Id == _currentUserId);

        var model = new ProfileInfoViewModel();
        if (dbProfile is null) return model;

        model.FirstName = dbProfile.FirstName;
        model.LastName = dbProfile.LastName;
        model.MiddleName = dbProfile.MiddleName;
        model.CountryId = dbProfile.Country;
        model.Address = dbProfile.Address;

        model.Country = (await _uow.AsyncRepository<CountryLookup>().GetByIdAsync(model.CountryId)).Name;

        var dbAccounts = (await _uow.AsyncRepository<BankDetails>().ListAsync(x => x.UserId == _currentUserId));

        if (dbAccounts is null) model.BankAccounts = new List<BankDetailsModel>();

        model.BankAccounts = dbAccounts.Select(x => new BankDetailsModel()
        {
            Id = x.Id,
            BankName = x.BankName,
            BankAccountNumber = x.BankAccountNumber,
            Balance = x.Balance
        });

        return model;
    }

    public async Task<bool> AddAccount(BankDetailsModel model)
    {
        var bankAccount = new BankDetails();
        bankAccount.BankAccountNumber = model.BankAccountNumber;
        bankAccount.BankName = model.BankName;
        bankAccount.Balance = null;
        bankAccount.UserId = _currentUserId;
        await _uow.AsyncRepository<BankDetails>().AddAsync(bankAccount);
        return await _uow.CommitAsync();
    }

    public async Task<string> GetCurrency()
    {
        return await GetCurrency(_currentUserId);
    }

    public async Task<bool> AccountBelongsToUser(int bankAccountId)
    {
        var account = await _uow.AsyncRepository<BankDetails>().GetByIdAsync(bankAccountId);
        if (account is null) return false;
        return account.UserId == _currentUserId;
    }

    public async Task<bool> LoadBalance(int bankAccountId, decimal balance)
    {
        var account = await _uow.AsyncRepository<BankDetails>().GetByIdAsync(bankAccountId);
        if (account is null) return false;

        account.Balance = (account.Balance ?? 0) + balance;
        return await _uow.CommitAsync();
    }

    public async Task<TransferAmountViewModel> GenerateTransferModel(int bankAccountId)
    {
        var dbModel = await _uow.AsyncRepository<UvwTransferModel>().GetSingleBySpec(x => x.BankAccountId == bankAccountId);

        var model = new TransferAmountViewModel()
        {
            Sender = dbModel.Sender,
            SenderBalance = dbModel.Balance,
            SenderBankName = dbModel.BankName,
            SenderCurrency = dbModel.Currency,
            SenderBankNumber = dbModel.BankAccountNumber,
            SenderBankAccountId = dbModel.BankAccountId
        };

        return model;
    }

    public async Task<bool> AccountExists(int checkAccount, TransferAmountViewModel model)
    {
        var accounts = await _uow.AsyncRepository<UvwTransferModel>()
            .ListAsync(x => x.BankAccountNumber == model.BankAccountNumber);
        var account = accounts?.Where(x => x.BankName.Trim() == model.BankName.Trim()).FirstOrDefault();
        if (account is null) return false;
        return account.BankAccountId != checkAccount;
    }

    public async Task<bool> BalanceEnough(int checkAccount, decimal? amount)
    {
        var account = await _uow.AsyncRepository<BankDetails>().GetByIdAsync(checkAccount);
        if (account is null) return false;
        return (account.Balance ?? 0) >= (amount ?? 0);
    }

    public async Task<ConfirmTransferModel> GetTransferModel(int fromId, TransferAmountViewModel model)
    {
        var senderDetail = await _uow.AsyncRepository<UvwTransferModel>().GetSingleBySpec(x => x.BankAccountId == fromId);
        var receiverId = await GetAccountId(model.BankAccountNumber, model.BankName);
        var receiverDetail = await _uow.AsyncRepository<UvwTransferModel>().GetSingleBySpec(x => x.BankAccountId == receiverId);

        decimal payoutAmount = await CalculatePayoutAmount(senderDetail.Currency, receiverDetail.Currency, model.Amount ?? 0);

        var confirmModel = new ConfirmTransferModel()
        {
            ReceiverDetail = receiverDetail,
            SenderDetail = senderDetail,
            TransferAmount = model.Amount ?? 0,
            TransferAmountString = string.Concat(senderDetail.Currency, " ", model.Amount.ToString()),
            PayoutAmount = payoutAmount,
            PayoutAmountString = $"{receiverDetail.Currency} {payoutAmount.ToString()}",
            TransactionDate = DateTime.UtcNow
        };

        return confirmModel;
    }

    public async Task<bool> ConfirmTransfer(int fromId, TransferAmountViewModel model)
    {
        var senderDetail = await _uow.AsyncRepository<UvwTransferModel>().GetSingleBySpec(x => x.BankAccountId == fromId);
        var receiverId = await GetAccountId(model.BankAccountNumber, model.BankName);
        var receiverDetail = await _uow.AsyncRepository<UvwTransferModel>().GetSingleBySpec(x => x.BankAccountId == receiverId);

        var senderAccount = await _uow.AsyncRepository<BankDetails>().GetByIdAsync(fromId);
        var receiverAccount = await _uow.AsyncRepository<BankDetails>().GetByIdAsync(receiverId);

        if (senderAccount is null || receiverAccount is null) return false;
        
        decimal payoutAmount = await CalculatePayoutAmount(senderDetail.Currency, receiverDetail.Currency, model.Amount ?? 0);

        senderAccount.Balance -= model.Amount;
        receiverAccount.Balance = (receiverAccount.Balance ?? 0) + payoutAmount;

        var transaction = new TransactionInfo()
        {
            SenderId = fromId,
            ReceiverId = receiverId,
            TransferAmount = model.Amount ?? 0,
            PayoutAmount = payoutAmount,
            TransactionDate = DateTime.UtcNow
        };

        await _uow.AsyncRepository<TransactionInfo>().AddAsync(transaction);
        return await _uow.CommitAsync();
    }

    private async Task<string> GetCurrency(int userId)
    {
        var user = await _uow.AsyncRepository<UserProfile>().GetByIdAsync(_currentUserId);
        var country = await _uow.AsyncRepository<CountryLookup>().GetByIdAsync(user.Country);
        return country.Code;
    }

    private async Task<int> GetAccountId(string accountNumber, string fullName)
    {
        var accounts = await _uow.AsyncRepository<UvwTransferModel>()
            .ListAsync(x => x.BankAccountNumber == accountNumber);
        var account = accounts?.Where(x => x.BankName.Trim() == fullName.Trim()).FirstOrDefault();
        if (account is null) return 0;
        return account.BankAccountId;
    }

    private async Task<decimal> CalculatePayoutAmount(string sourceCurrency, string destCurrency, decimal amount)
    {
        if (string.Equals(sourceCurrency, destCurrency)) return amount;

        var list = await _exchangeRateService.GetExchangeRate(DateTime.Now);

        var listItem = list.Where(x => x.CurrencyCode == sourceCurrency)?.FirstOrDefault();
        if (listItem is null) return amount;

        return ((amount/listItem?.Unit) * Decimal.Parse(listItem?.Sell ?? string.Empty)) ?? 0;
    }
}