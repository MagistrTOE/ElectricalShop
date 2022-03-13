using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Carts.GetPrice
{
    public class GetPriceRequest : IRequest<GetPriceResponse>
    {
        public Guid UserId { get; set; }

        public GetPriceRequest(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetPriceHandler : IRequestHandler<GetPriceRequest, GetPriceResponse>
    {
        private readonly ICartRepository _cartRepository;

        public GetPriceHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<GetPriceResponse> Handle(GetPriceRequest request, CancellationToken cancellationToken)
        {
            var cartWithProducts = await _cartRepository.GetByUserId(request.UserId);
            var price = cartWithProducts.GetAllPrice();

            return new GetPriceResponse
            {
                AllPrice = price
            };
        }
    }
}