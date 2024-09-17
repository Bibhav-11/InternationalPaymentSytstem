using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

public class BankDetailsModel
{
    public int Id { get; set; }
    [Display(Name = "Bank Name")]
    [Required(ErrorMessage = "{0} is required")]
    public string BankName { get; set; } = string.Empty;

    [Display(Name = "Bank Account Number")]
    [Required(ErrorMessage = "{0} is required")]
    public string BankAccountNumber { get; set; } = string.Empty;
    
    public decimal? Balance { get; set; }
}