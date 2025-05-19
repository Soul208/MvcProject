using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Passwrod Is required")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Passwrod Is required")]
        public string ConfirmPassword { get; set; }
    }
}
