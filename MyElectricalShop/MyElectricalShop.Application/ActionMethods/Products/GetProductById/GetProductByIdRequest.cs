using AutoMapper;
using MediatR;
using MyElectricalShop.Application.ActionMethods.Products.GetProductList;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.Products.GetProductById
{
    public class GetProductByIdRequest : IRequest<ProductResponse>
    {
        public Guid Id { get; set; }

        public GetProductByIdRequest(Guid id)
        {
            Id = id;
        }
    }

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(GetProductByIdRequest request, CancellationToken token)
        {
            var arrayProperties = new[]
            {
                nameof(Company),
                nameof(Category),
                nameof(VoltageLevel)
            };
            var productById = await _productRepository.GetById(request.Id, arrayProperties);

            return _mapper.Map<ProductResponse>(productById);
        }
    }
}
