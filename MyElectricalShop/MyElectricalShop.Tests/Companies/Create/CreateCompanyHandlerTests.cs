using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.Companies;
using MyElectricalShop.Application.ActionMethods.Companies.Create;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Companies.Create
{
    public class CreateCompanyHandlerTests
    {
        private readonly Mock<ICompanyRepository> _ICompanyRepositoryMock;
        private readonly CreateCompanyHandler _handler;

        public CreateCompanyHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CompanyMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _ICompanyRepositoryMock = new Mock<ICompanyRepository>();
            _handler = new CreateCompanyHandler(mapper, _ICompanyRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCompany_Success()
        {
            // Arrange
            _ICompanyRepositoryMock.Setup(x => x.Add(It.IsAny<Company>()));
            var request = new CreateCompanyRequest() { Name = "TestName", Country = "TestCountry" };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<CreatedCompanyResponse>(result);
            Assert.Equal(request.Name, result.Name);
            _ICompanyRepositoryMock.Verify(x => x.Add(It.IsAny<Company>()), Times.AtMostOnce);
        }
    }
}
