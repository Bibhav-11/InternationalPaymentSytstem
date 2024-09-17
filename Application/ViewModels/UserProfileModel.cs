using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

public class UserProfileModel
{
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Middle Name")]
    public string? MiddleName { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    
    public string? Address { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    public int Country { get; set; }
}