using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessage = "As senhas devem coincidir.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }

    }
}