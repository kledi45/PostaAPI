using System.ComponentModel.DataAnnotations;

namespace PostaAPI.DTOs.Users
{
    public class CreateUserDTO
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(100)]
        public string? UserName { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Password { get; set; }

        [StringLength(255)]
        public string? SaltedPassword { get; set; }
        public IFormFile? Document { get; set; }
        public int IdRole { get; set; }
    }
}
