using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Products.GetProductList
{
    public class GetProductListRequest : IRequest<List<ProductResponse>>
    {
    }
    
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
            var productsList = await _productRepository.GetProductsListWithFullInfo();

            return _mapper.Map<List<ProductResponse>>(productsList);
        }
    }
}
