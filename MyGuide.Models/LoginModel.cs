using System.ComponentModel.DataAnnotations;

namespace MyGuide.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Парола:")]
        public string Password { get; set; }
    }
}
