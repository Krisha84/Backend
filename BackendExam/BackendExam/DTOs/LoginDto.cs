using System.ComponentModel.DataAnnotations;

namespace BackendExam.DTOs
{
    public class LoginDto
    {
        [Required]
        public string email { get; set; } = null!;

        [Required]
        public string password { get; set; } = null!;
    }
}
