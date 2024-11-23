using System.ComponentModel.DataAnnotations;

namespace PostaAPI.Classes
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public int IdEntryUser { get; set; }
        public string? EntryUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? IdUpdateUser { get; set; }
        public string? UpdateUser { get; set; }
        public bool IsDeleted { get; set; }
        public int? IdDeleteUser { get; set; }
        public DateTime? DeleteDate  { get; set; }

    }
}
