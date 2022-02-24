using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.Products.Create
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
            var product = _mapper.Map<Product>(request);
            await _productRepository.Add(product);

            return _mapper.Map<CreatedProductResponse>(product);
        }
    }
}
