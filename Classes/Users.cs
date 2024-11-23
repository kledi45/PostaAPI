using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostaAPI.Classes
{
    [Serializable]
    public class Users : BaseEntity
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
        public string? ProfileImagePath { get; set; }

        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Roles? IdRoleNavigation { get; set; }

    }
}
