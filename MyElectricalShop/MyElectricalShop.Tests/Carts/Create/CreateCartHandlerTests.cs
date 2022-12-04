using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.Carts;
using MyElectricalShop.Application.ActionMethods.Carts.Create;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Carts.Create
{
    public class CreateCartHandlerTests
    {
        private readonly Mock<ICartRepository> _ICartRepositoryMock;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CartMapper>());
            var mapper = new Mapper(mapperConfiguration);
            
            _ICartRepositoryMock = new Mock<ICartRepository>();
            _handler = new CreateCartHandler(_ICartRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task CreateCart()
        {
            // Arrange
            _ICartRepositoryMock.Setup(x => x.Add(It.IsAny<Cart>()));

            var request = new CreateCartRequest() { UserId = new Guid("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE") };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(request.UserId, result.UserId);
            _ICartRepositoryMock.Verify(x => x.Add(It.IsAny<Cart>()));
        }
    }
}
