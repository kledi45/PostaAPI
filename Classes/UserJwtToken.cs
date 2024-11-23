using System.ComponentModel.DataAnnotations;

namespace PostaAPI.Classes
{
    public class UserJwtToken
    {
        [Key]
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int IdUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
