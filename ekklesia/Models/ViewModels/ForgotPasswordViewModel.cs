using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
