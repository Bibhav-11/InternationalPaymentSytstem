namespace Application.ViewModels;

public class TransactionInfoViewModel
{
    public int TransactionId { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public decimal TransferAmount { get; set; }
    public decimal PayoutAmount { get; set; }
    public string TransferAmountString { get; set; }
    public string PayoutAmountString { get; set; }
    public string Sender { get; set; }
    public string SenderAddress { get; set; }
    public string SenderCountry { get; set; }
    public string SenderBank { get; set; }
    public string SenderBankNumber { get; set; }
    public string Receiver { get; set; }
    public string ReceiverAddress { get; set; }
    public string ReceiverCountry { get; set; }
    public string ReceiverBank { get; set; }
    public string ReceiverBankNumber { get; set; }
    
    
    
}