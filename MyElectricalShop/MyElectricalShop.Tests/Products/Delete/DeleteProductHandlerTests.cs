using Core.Exceptions;
using MediatR;
using Moq;
using MyElectricalShop.Application.ActionMethods.Products.Delete;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Products.Delete
{
    public class DeleteProductHandlerTests
    {
        private readonly Mock<IProductRepository> _IProductRepositoryMock;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _IProductRepositoryMock = new Mock<IProductRepository>();
            _handler = new DeleteProductHandler(_IProductRepositoryMock.Object);
        }

        [Fact]
        public async Task DeleteProduct_Success()
        {
            // Arrange
            _IProductRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new Product());

            _IProductRepositoryMock.Setup(x => x.Delete(It.IsAny<Product>()));

            var request = new DeleteProductRequest(It.IsAny<Guid>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Unit>(result);
            _IProductRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
            _IProductRepositoryMock.Verify(x => x.Delete(It.IsAny<Product>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task DeleteCompany_NotFoundException()
        {
            // Arrange
            _IProductRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((Product)null);

            var request = new DeleteProductRequest(It.IsAny<Guid>());

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal($"Сущность с заданным Id: {request.Id} не найдена.", exception.Message);
        }
    }
}
