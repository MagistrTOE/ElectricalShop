using MediatR;
using AutoMapper;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Carts.Clear
{
    public class ClearCartRequest : IRequest
    {
        public Guid UserId { get; set; }

        public ClearCartRequest(Guid userId)
        {
            UserId = userId;
        }
    }

    public class ClearCartHandler : IRequestHandler<ClearCartRequest>
    {
        private readonly ICartRepository _cartRepository;

        public ClearCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Unit> Handle(ClearCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserId(request.UserId);
            cart.CartLines.Clear();
           
            await _cartRepository.Update(cart);

            return Unit.Value;
        }
    }
}
