using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.Companies;
using MyElectricalShop.Application.ActionMethods.Companies.GetList;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Companies.GetList
{
    public class GetCompanyListHandlerTests
    {
        private readonly Mock<ICompanyRepository> _ICompanyRepositoryMock;
        private readonly GetCompanyListHandler _handler;

        public GetCompanyListHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CompanyMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _ICompanyRepositoryMock = new Mock<ICompanyRepository>();
            _handler = new GetCompanyListHandler(_ICompanyRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetCompanyList()
        {
            // Arrange
            _ICompanyRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<Company>() {
                    new Company() { Id = It.IsAny<int>(), Name = "TestName", Country = "TestCountry" } 
                });

            var request = new GetCompanyListRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<List<CompanyResponseItem>>(result);
            
            var resultItem = result.First();
            Assert.Equal("TestName", resultItem.Name);
            Assert.Equal("TestCountry", resultItem.Country);
        }
    }
}
