using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.User
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
