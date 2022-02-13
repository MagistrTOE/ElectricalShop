using MediatR;

namespace MyElectricalShop.Application.ActionMethods.Product.Create
{
    public class CreateProductRequest : IRequest<CreatedProductResponse>
    {
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public int VoltageLevelId { get; set; }
        public decimal Price { get; set; }
    }
}
