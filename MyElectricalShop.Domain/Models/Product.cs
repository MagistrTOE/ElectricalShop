using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElectricalShop.Domain.Models
{
    public class Product
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
