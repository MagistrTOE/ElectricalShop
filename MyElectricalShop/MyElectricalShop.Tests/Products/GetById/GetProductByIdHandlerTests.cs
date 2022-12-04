using AutoMapper;
using Core.Exceptions;
using Moq;
using MyElectricalShop.Application.ActionMethods.Products;
using MyElectricalShop.Application.ActionMethods.Products.GetProductById;
using MyElectricalShop.Application.ActionMethods.Products.GetProductList;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Products.GetById
{
    public class GetProductByIdHandlerTests
    {
        private readonly Mock<IProductRepository> _IProductRepositoryMock;
        private readonly GetProductByIdHandler _handler;

        public GetProductByIdHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _IProductRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductByIdHandler(_IProductRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetProductById_Succes()
        {
            // Arrange
            _IProductRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new Product()
                {
                    Id = new Guid("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE"),
                    CategoryId = It.IsAny<int>(),
                    CompanyId = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    Power = It.IsAny<double>(),
                    VoltageLevelId = It.IsAny<int>(),
                    Price = It.IsAny<decimal>()
                });

            var request = new GetProductByIdRequest(new Guid("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE"));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(request.Id, result.Id);
            _IProductRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task GetProductById_NotFoundException()
        {
            // Arrange
            _IProductRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((Product)null);

            var request = new GetProductByIdRequest(It.IsAny<Guid>());

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            _IProductRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
            Assert.Equal($"Продукт с заданным Id: {request.Id} не найден.", exception.Message);
        }
    }
}
