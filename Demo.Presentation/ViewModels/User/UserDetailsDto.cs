using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.User
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }
}
