using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElectricalShop.Domain.Models
{
    public class CartLine
    {
        public Guid Id { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; }

    }
}
