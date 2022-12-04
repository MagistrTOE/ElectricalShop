using MediatR;
using Moq;
using MyElectricalShop.Application.ActionMethods.Carts.Clear;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Carts.Clear
{
    public class ClearCartHandlerTests
    {
        private readonly Mock<ICartRepository> _ICartRepositoryMock;
        private readonly ClearCartHandler _handler;

        public ClearCartHandlerTests()
        {
            _ICartRepositoryMock = new Mock<ICartRepository>();
            _handler = new ClearCartHandler(_ICartRepositoryMock.Object);
        }

        [Fact]
        public async Task ClearCart()
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
                    }
                });

            _ICartRepositoryMock.Setup(x => x.Update(It.IsAny<Cart>()));
            
            var request = new ClearCartRequest(It.IsAny<Guid>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Unit>(result);
            _ICartRepositoryMock.Verify(x => x.GetByUserId(It.IsAny<Guid>()), Times.AtMostOnce);
            _ICartRepositoryMock.Verify(x => x.Update(It.IsAny<Cart>()), Times.AtMostOnce);
        }
    }
}
