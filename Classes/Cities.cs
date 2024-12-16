using System.ComponentModel.DataAnnotations.Schema;

namespace PostaAPI.Classes
{
    public class Cities : BaseEntity
    {
        public Cities()
        {
            Shipments = new HashSet<Shipments>();
        }
        public string? Name { get; set; }
        public int IDCountry { get; set; }
        [ForeignKey("IDCountry")]
        public virtual Countries? IdCountryNavigation { get; set; }
        public  virtual ICollection<Shipments> Shipments { get; set; }
    }
}
