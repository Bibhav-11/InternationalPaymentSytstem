namespace Domain.Entities.Application;

public class TransactionInfo
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public decimal TransferAmount { get; set; }
    public decimal PayoutAmount { get; set; }
    public DateTime TransactionDate { get; set; }
}