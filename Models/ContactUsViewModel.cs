using System.ComponentModel.DataAnnotations;

public class ContactUsViewModel
{
    [Required]
    [Display(Name = "Name")]
    public string?Name { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string?Email { get; set; }

    [Required]
    [Display(Name = "Comment")]
    public string?Comment { get; set; }
}
