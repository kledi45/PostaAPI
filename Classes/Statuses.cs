using System.Collections;

namespace PostaAPI.Classes
{
    public class Statuses : BaseEntity
    {
        public Statuses() => Shipments = new HashSet<Shipments>();
        public string? Name { get; set; }
        public virtual ICollection<Shipments> Shipments { get; set; } 
    }
}
