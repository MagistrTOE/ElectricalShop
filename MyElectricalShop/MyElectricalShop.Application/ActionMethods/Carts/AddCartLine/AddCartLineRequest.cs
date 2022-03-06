using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;
using Core.Exceptions;

namespace MyElectricalShop.Application.ActionMethods.Carts.AddCartLine
{
    public class AddCartLineRequest : IRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddCartLineHandler : IRequestHandler<AddCartLineRequest>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public AddCartLineHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<Unit> Handle(AddCartLineRequest request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<CartLine>(request);
            var cart = await _cartRepository.GetByUserId(request.UserId);
            //var existLine = cart.CartLines.FirstOrDefault(x => x.ProductId == request.ProductId);
            //if (existLine != null)
            //    throw new ArgumentNotFoundException($"В корзине продукт с Id: {request.ProductId}, уже существует.");

            cart.CartLines.Add(item);

            await _cartRepository.Update(cart);

            return Unit.Value;
        }
    }
}
