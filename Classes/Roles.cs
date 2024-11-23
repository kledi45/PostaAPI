using PostaAPI.Classes;

namespace PostaAPI.Classes
{
    [Serializable]
    public class Roles : BaseEntity
    {
        public Roles() => Users = new HashSet<Users>();   
        public string? Title { get; set; }
        public virtual ICollection<Users> Users { get; set; } 
    }
}
