using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembre-se de mim")]
        public bool RememberMe { get; set; }
    }
}
