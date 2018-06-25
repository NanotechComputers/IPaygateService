namespace Paygate.Models.Shared
{
    public class OrderItems
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductRisk { get; set; }
        public int OrderQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Currencies Currency { get; set; }
    }
}