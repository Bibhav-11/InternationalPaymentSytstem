namespace Application.ViewModels;

public class ProfileInfoViewModel
{
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public int CountryId { get; set; }

    public IEnumerable<BankDetailsModel> BankAccounts { get; set; } = new List<BankDetailsModel>();
}