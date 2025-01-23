using System.ComponentModel.DataAnnotations;

namespace FullCourtInsights.Models
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirmed Password is Required")]
        public string? ConfPassword { get; set; }

    }
}
