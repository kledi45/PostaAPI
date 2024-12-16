using System.ComponentModel.DataAnnotations;

namespace PostaAPI.DTOs.Users
{
    public class UsersListDTO
    {
        public int Id { get; set; }
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
        public string? ProfileImagePath { get; set; }
        public int IdRole { get; set; }
        public string? Role { get; set; }
        public int Prefix { get; set; }
        public long PhoneNumber { get; set; }
        public int IdCountry { get; set; }
        public string? Country { get; set; }
        public string? FullName => FirstName + " " + LastName;
    }
}
