namespace MyElectricalShop.Application.ActionMethods.Product.GetProductList
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string ProductionCompany { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public ushort Voltage { get; set; }
        public decimal Price { get; set; }
    }
}
