namespace MyElectricalShop.Domain.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public IQueryable<CartLine> CartLines { get; set; }
    }
}
