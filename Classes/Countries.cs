namespace PostaAPI.Classes
{
    public class Countries : BaseEntity
    {
        public Countries() 
        {
            Users = new HashSet<Users>();
            Cities = new HashSet<Cities>();
            Shipments = new HashSet<Shipments>();
        }
        public string? Name { get; set; }
        public decimal? ShippingPrice { get; set; }
        public ICollection<Users> Users { get; set; }
        public ICollection<Cities> Cities { get; set; }
        public ICollection<Shipments> Shipments { get; set; }

    }
}
