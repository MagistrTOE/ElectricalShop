using AutoMapper;
using Core.Exceptions;
using Moq;
using MyElectricalShop.Application.ActionMethods.Carts;
using MyElectricalShop.Application.ActionMethods.Carts.UpdateCartLine;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Carts.UpdateCartLine
{
    public class UpdateCartLineHandleTests
    {
        private readonly Mock<ICartRepository> _ICartRepositoryMock;
        private readonly UpdateCartLineHandle _handler;

        public UpdateCartLineHandleTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CartMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _ICartRepositoryMock = new Mock<ICartRepository>();
            _handler = new UpdateCartLineHandle(_ICartRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task UpdateCart_Success()
        {
            // Arrange
            _ICartRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new Cart()
                {
                    CartLines = new List<CartLine>() {
                        new CartLine { ProductId = It.IsAny<Guid>(), Quantity = It.IsAny<int>() }
                    }
                });

            _ICartRepositoryMock.Setup(x => x.Update(It.IsAny<Cart>()));

            var request = new UpdateCartLineRequest()
            {
                CartId = It.IsAny<Guid>(),
                ProductId = It.IsAny<Guid>(),
                Quantity = It.IsAny<int>()
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<UpdateCartLineResponse>(result);
            _ICartRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
            _ICartRepositoryMock.Verify(x => x.Update(It.IsAny<Cart>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task UpdateCart_CartByIdNotFound()
        {
            // Arrange
            _ICartRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((Cart)null);
            
            var request = new UpdateCartLineRequest()
            {
                CartId = It.IsAny<Guid>(),
                ProductId = It.IsAny<Guid>(),
                Quantity = It.IsAny<int>()
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal($"Корзина с заданным Id: {request.CartId}, не найдена.", exception.Message);
            _ICartRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
        }
        
        [Fact]
        public async Task UpdateCart_ProductByIdNotFound()
        {
            // Arrange
            _ICartRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new Cart()
                {
                    CartLines = new List<CartLine>() {
                        new CartLine { ProductId = It.IsAny<Guid>(), Quantity = It.IsAny<int>() }
                    }
                });
            var request = new UpdateCartLineRequest()
            {
                CartId = It.IsAny<Guid>(),
                ProductId = new Guid("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE"),
                Quantity = It.IsAny<int>()
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal($"Продукт с заданным Id: {request.ProductId}, не найден.", exception.Message);
            _ICartRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
        }
    }
}
