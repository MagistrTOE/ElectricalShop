using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Models;
using MyElectricalShop.Domain.Interfaces;
using Core.Exceptions;

namespace MyElectricalShop.Application.ActionMethods.Carts.UpdateCartLine
{
    public class UpdateCartLineRequest : IRequest<UpdateCartLineResponse>
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartLineHandle : IRequestHandler<UpdateCartLineRequest, UpdateCartLineResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public UpdateCartLineHandle(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCartLineResponse> Handle(UpdateCartLineRequest request, CancellationToken cancellationToken)
        {
            var arrayProperties = new[] 
            {
                nameof(Cart.CartLines),
            };

            var cart = await _cartRepository.GetById(request.CartId, arrayProperties);
            if (cart == null)
                throw new ArgumentNotFoundException($"Корзина с заданным Id: {request.CartId}, не найдена.");

            var line = cart.CartLines.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (line == null)
                throw new ArgumentNotFoundException($"Продукт с заданным Id: {request.ProductId}, не найден.");

            line.Quantity = request.Quantity;

            await _cartRepository.Update(cart);

            return _mapper.Map<UpdateCartLineResponse>(line);
        }
    }
}
