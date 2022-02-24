namespace MyElectricalShop.Application.ActionMethods.Products.GetProductList
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public CategoryResponseForProduct Category { get; set; }
        public CompanyResponseForProduct Company { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public VoltageLevelResponseForProduct VoltageLevel { get; set; }
        public decimal Price { get; set; }
    }

    public class CategoryResponseForProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyResponseForProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class VoltageLevelResponseForProduct
    {
        public int Id { get; set; }
        public string Level { get; set; }
    }
}
