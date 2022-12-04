using Core.Exceptions;
using MediatR;
using Moq;
using MyElectricalShop.Application.ActionMethods.Companies.Delete;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.Companies.Delete
{
    public class DeleteCompanyHandlerTests
    {
        private readonly Mock<ICompanyRepository> _ICompanyRepositoryMock;
        private readonly DeleteCompanyHandler _handler;

        public DeleteCompanyHandlerTests()
        {
            _ICompanyRepositoryMock = new Mock<ICompanyRepository>();
            _handler = new DeleteCompanyHandler(_ICompanyRepositoryMock.Object);
        }

        [Fact]
        public async Task DeleteCompany_Success()
        {
            // Arrange
            _ICompanyRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new Company());

            _ICompanyRepositoryMock.Setup(x => x.Delete(It.IsAny<Company>()));

            var request = new DeleteCompanyRequest(It.IsAny<int>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Unit>(result);
            _ICompanyRepositoryMock.Verify(x => x.GetById(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
            _ICompanyRepositoryMock.Verify(x => x.Delete(It.IsAny<Company>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task DeleteCompany_NotFoundException()
        {
            // Arrange
            _ICompanyRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((Company)null);

            var request = new DeleteCompanyRequest(It.IsAny<int>());

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal($"Сущность с заданным Id: {request.Id} не найдена.", exception.Message);
        }
    }
}
