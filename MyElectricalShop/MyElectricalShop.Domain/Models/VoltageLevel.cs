using Data.Entities;

namespace MyElectricalShop.Domain.Models
{
    public class VoltageLevel : ModelBase<int>
    {
        public string Level { get; set; }
    }
}
