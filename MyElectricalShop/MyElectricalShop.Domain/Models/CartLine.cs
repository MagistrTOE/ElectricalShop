using Data.Entities;

namespace MyElectricalShop.Domain.Models
{
    public class CartLine : ModelBase<Guid> 
    {
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Guid CartId { get; set; }
    }
}
