using System.ComponentModel.DataAnnotations.Schema;

namespace PostaAPI.Classes
{
    public class Shipments : BaseEntity
    {
        public int IDUser { get; set; }
        public string? Receiver { get; set; }
        public string? PhoneNumber { get; set; }
        public int IDCountry { get; set; }
        public int IDCity { get; set; }
        public string? ItemDescription { get; set; }
        public string? ExtraItemDescription { get; set; }
        public bool CanBeOpened { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal Total { get; set; }
        public string? Address { get; set; }
        public string? UniqueIdentifier { get; set; }
        public int? IdStatus { get; set; }

        [ForeignKey("IDUser")]
        public virtual Users? IdUserNavigation { get; set; }
        [ForeignKey("IDCountry")]
        public virtual Countries? IdCountryNavigation { get; set; }
        [ForeignKey("IDCity")]
        public virtual Cities? IdCityNavigation { get; set; }
        [ForeignKey("IdStatus")]
        public virtual Statuses? IdStatusNavigation { get; set; }

    }
}
