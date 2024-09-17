namespace Domain.Entities;

public class UvwTransferModel
{
    public string Sender { get; set; }
    public string Currency { get; set; }
    public int BankAccountId { get; set; }
    public string BankName { get; set; }
    public string BankAccountNumber { get; set; }
    public decimal? Balance { get; set; }
}