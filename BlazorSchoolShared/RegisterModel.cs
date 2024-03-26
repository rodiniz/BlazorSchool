using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class RegisterModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
