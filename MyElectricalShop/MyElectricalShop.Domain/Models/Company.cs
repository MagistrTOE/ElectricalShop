using Data.Entities;

namespace MyElectricalShop.Domain.Models
{
    public class Company : ModelBase<int>
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
