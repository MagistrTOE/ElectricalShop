using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.Products;
using MyElectricalShop.Application.ActionMethods.Products.GetProductList;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Products.GetList
{
    public class GetProductListHandlerTests
    {
        private readonly Mock<IProductRepository> _IProductRepositoryMock;
        private readonly GetProductListHandler _handler;

        public GetProductListHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _IProductRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductListHandler(_IProductRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetProductList()
        {
            // Assert
            _IProductRepositoryMock
                .Setup(x => x.GetProductsListWithFullInfo())
                .ReturnsAsync(new List<Product>() {
                    new Product() {
                        Id = It.IsAny<Guid>(),
                        CategoryId = It.IsAny<int>(),
                        CompanyId = It.IsAny<int>(),
                        Name = "TestName",
                        Power = It.IsAny<double>(),
                        VoltageLevelId = It.IsAny<int>(),
                        Price = It.IsAny<decimal>()
                    }
                });
            var request = new GetProductListRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<List<ProductResponse>>(result);

            var resultItem = result.First();
            Assert.Equal("TestName", resultItem.Name);
            _IProductRepositoryMock.Verify(x => x.GetProductsListWithFullInfo(), Times.AtMostOnce);
        }
    }
}
