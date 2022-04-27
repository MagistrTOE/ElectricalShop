using MassTransit;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;
using MyElectricalShop.Shared.ExternalEvents;

namespace MyElectricalShop.Application.RabbitMQ
{
    public class CreateCartConsumer : IConsumer<CreatedCart>
    {
        private readonly ICartRepository _cartRepository;

        public CreateCartConsumer(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Consume(ConsumeContext<CreatedCart> context)
        {
            var createdCart = new Cart { UserId = context.Message.UserId };

            await _cartRepository.Add(createdCart);
        }
    }
}
