using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using ProductEntity = MyElectricalShop.Domain.Models.Product;

namespace MyElectricalShop.Application.ActionMethods.Product.AddProduct
{
    public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public AddProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity();
            product.Category = request.Category;
            product.ProductionCompany = request.ProductionCompany;
            product.Name = request.Name;
            product.Power = request.Power;
            product.Voltage = request.Voltage;
            product = await _productRepository.AddProduct(product);

            return _mapper.Map<AddProductResponse>(product);
        }
    }
}
