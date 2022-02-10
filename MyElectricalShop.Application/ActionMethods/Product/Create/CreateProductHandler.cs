using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using ProductEntity = MyElectricalShop.Domain.Models.Product;

namespace MyElectricalShop.Application.ActionMethods.Product.Create
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CreatedProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(request);
            await _productRepository.AddProduct(product);

            return _mapper.Map<CreatedProductResponse>(product);
        }
    }
}
