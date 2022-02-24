using Data.Entities;

namespace MyElectricalShop.Domain.Models
{
    public class Product : ModelBase<Guid>
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public int VoltageLevelId { get; set; }
        public VoltageLevel VoltageLevel { get; set; }
        public decimal Price { get; set; }
    }
}
