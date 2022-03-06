using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.Carts.Create
{
    public class CreateCartRequest : IRequest<CreatedCartResponse>
    {
        public Guid UserId { get; set; }
    }

    public class CreateCartHandler : IRequestHandler<CreateCartRequest, CreatedCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public CreateCartHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<CreatedCartResponse> Handle(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<Cart>(request);
            await _cartRepository.Add(cart);

            return _mapper.Map<CreatedCartResponse>(cart);
        }
    }
    

}
