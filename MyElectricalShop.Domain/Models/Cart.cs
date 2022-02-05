namespace MyElectricalShop.Domain.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<CartLine> CartLines { get; set; }
    }
}
