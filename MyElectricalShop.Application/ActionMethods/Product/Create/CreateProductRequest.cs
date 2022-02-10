using MediatR;

namespace MyElectricalShop.Application.ActionMethods.Product.Create
{
    public class CreateProductRequest : IRequest<CreatedProductResponse>
    {
        public string Category { get; set; }
        public string ProductionCompany { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public ushort Voltage { get; set; }
        public decimal Price { get; set; }
    }
}
