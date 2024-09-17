using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

public class TransferAmountViewModel
{
    public string? Sender { get; set; }
    public string? SenderCurrency { get; set; }
    public int SenderBankAccountId { get; set; }
    public string? SenderBankName { get; set; }
    public string? SenderBankNumber { get; set; }
    public decimal? SenderBalance { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Receiver's Account Number")]
    public string BankAccountNumber { get; set; } = null!;
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Receiver's Full Name")]
    public string FullName { get; set; } = null!;
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Receiver's Bank Name")]
    public string BankName { get; set; } = null!;
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Amount")]
    public decimal? Amount { get; set; }
}