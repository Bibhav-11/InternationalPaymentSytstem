using Domain.Entities;

namespace Application.ViewModels;

public class ConfirmTransferModel
{
    public UvwTransferModel SenderDetail { get; set; }
    public UvwTransferModel ReceiverDetail { get; set; }
    public decimal TransferAmount { get; set; }
    public string TransferAmountString { get; set; }
    public decimal PayoutAmount { get; set; }
    public string PayoutAmountString { get; set; }
    public DateTime TransactionDate { get; set; }
}