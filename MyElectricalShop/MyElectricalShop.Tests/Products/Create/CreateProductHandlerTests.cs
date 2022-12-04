using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.Products;
using MyElectricalShop.Application.ActionMethods.Products.Create;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Products.Create
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IProductRepository> _IProductRepositoryMock;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _IProductRepositoryMock = new Mock<IProductRepository>();
            _handler = new CreateProductHandler(_IProductRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task CreateCompany_Success()
        {
            // Arrange
            _IProductRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()));

            var request = new CreateProductRequest()
            {
                CategoryId = It.IsAny<int>(),
                CompanyId = It.IsAny<int>(),
                Name = "TestName",
                Power = It.IsAny<double>(),
                VoltageLevelId = It.IsAny<int>(),
                Price = It.IsAny<decimal>()
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<CreatedProductResponse>(result);
            Assert.Equal(request.Name, result.Name);
            _IProductRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.AtMostOnce);
        }
    }
}
