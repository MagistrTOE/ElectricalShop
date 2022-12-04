using AutoMapper;
using Core.Exceptions;
using MediatR;
using Moq;
using MyElectricalShop.Application.ActionMethods.Carts;
using MyElectricalShop.Application.ActionMethods.Carts.AddCartLine;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Carts.AddCartLine
{
    public class AddCartLineHandlerTests
    {
        private readonly Mock<ICartRepository> _ICartRepositoryMock;
        private readonly AddCartLineHandler _handler;

        public AddCartLineHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CartMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _ICartRepositoryMock = new Mock<ICartRepository>();
            _handler = new AddCartLineHandler(_ICartRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task AddCartLine_Succes()
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

            var request = new AddCartLineRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Unit>(result);
            _ICartRepositoryMock.Verify(x => x.GetByUserId(It.IsAny<Guid>()), Times.AtMostOnce);
            _ICartRepositoryMock.Verify(x => x.Update(It.IsAny<Cart>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task AddCartLine_DatabaseException()
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

            _ICartRepositoryMock.Setup(x => x.Update(It.IsAny<Cart>()))
                .Returns(Task.FromException(new Exception()));

            var request = new AddCartLineRequest();

            // Act
            var exception = await Assert.ThrowsAsync<DatabaseException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal($"Продукт в корзине с Id: {request.ProductId}, уже существует.", exception.Message);
            _ICartRepositoryMock.Verify(x => x.GetByUserId(It.IsAny<Guid>()), Times.AtMostOnce);
            _ICartRepositoryMock.Verify(x => x.Update(It.IsAny<Cart>()), Times.AtMostOnce);
        }
    }
}
