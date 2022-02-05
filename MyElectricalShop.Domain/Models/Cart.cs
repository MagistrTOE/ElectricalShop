namespace MyElectricalShop.Domain.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public IEnumerable<CartLine> CartLines { get; set; }
    }
}
