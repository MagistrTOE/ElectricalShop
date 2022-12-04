using Moq;
using MyElectricalShop.Application.ActionMethods.Carts.GetPrice;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Carts.GetPrice
{
    public class GetPriceHandlerTests
    {
        private readonly Mock<ICartRepository> _ICartRepositoryMock;
        private readonly GetPriceHandler _handler;

        public GetPriceHandlerTests()
        {
            _ICartRepositoryMock = new Mock<ICartRepository>();
            _handler = new GetPriceHandler(_ICartRepositoryMock.Object);
        }

        [Fact]
        public async Task GetPrice()
        {
            // Arrange
            _ICartRepositoryMock
                .Setup(x => x.GetByUserId(It.IsAny<Guid>()))
                .ReturnsAsync(new Cart()
                {
                    Id = It.IsAny<Guid>(),
                    CartLines = new List<CartLine>()
                    {
                        new CartLine()
                        {
                            Quantity = 10,
                            Product = new Product() { Price = 100 }
                        },
                    }
                });

            var request = new GetPriceRequest(It.IsAny<Guid>());

            // Act 
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(1000, result.AllPrice);
            _ICartRepositoryMock.Verify(x => x.GetByUserId(It.IsAny<Guid>()), Times.AtMostOnce);
        }
    }
}
