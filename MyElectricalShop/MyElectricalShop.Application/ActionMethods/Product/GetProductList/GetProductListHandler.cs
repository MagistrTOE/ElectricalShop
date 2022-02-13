using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Product.GetProductList
{
    public class GetProductListHandler : IRequestHandler<GetProductListRequest, List<ProductResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductListHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetProductListRequest request, CancellationToken cancellationToken)
        {
            var productsList = await _productRepository.GetAllProducts();

            return _mapper.Map<List<ProductResponse>>(productsList);
        }
    }
}
