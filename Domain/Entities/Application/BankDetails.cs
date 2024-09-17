namespace Domain.Entities.Application;

public class BankDetails
{
    public int Id { get; set; }
    public string BankName { get; set; } = null!;
    public string BankAccountNumber { get; set; } = null!;
    public decimal? Balance { get; set; }
    
    public int UserId { get; set; }
}