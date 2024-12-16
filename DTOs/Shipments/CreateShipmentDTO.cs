namespace PostaAPI.DTOs.Shipments
{
    public class CreateShipmentDTO
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
    }
}
