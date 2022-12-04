using Core.Exceptions;
using MediatR;
using Moq;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Delete;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.VoltageLevels.Delete
{
    public class DeleteVoltageLevelHandlerTests
    {
        private readonly Mock<IVoltageLevelRepository> _IVoltageLevelRepositoryMock;
        private readonly DeleteVoltageLevelHandler _handler;

        public DeleteVoltageLevelHandlerTests()
        {
            _IVoltageLevelRepositoryMock = new Mock<IVoltageLevelRepository>();
            _handler = new DeleteVoltageLevelHandler(_IVoltageLevelRepositoryMock.Object);
        }

        [Fact]
        public async Task DeleteVoltageLevel_Success()
        {
            // Arrange
            _IVoltageLevelRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new VoltageLevel());
           
            _IVoltageLevelRepositoryMock.Setup(x => x.Delete(It.IsAny<VoltageLevel>()));

            var request = new DeleteVoltageLevelRequest(It.IsAny<int>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Unit>(result);
            _IVoltageLevelRepositoryMock.Verify(x => x.GetById(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()), Times.AtMostOnce);
            _IVoltageLevelRepositoryMock.Verify(x => x.Delete(It.IsAny<VoltageLevel>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task DeleteVoltageLevel_NotFoundException()
        {
            // Arrange
            _IVoltageLevelRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((VoltageLevel)null);

            _IVoltageLevelRepositoryMock.Setup(x => x.Delete(It.IsAny<VoltageLevel>()));

            var request = new DeleteVoltageLevelRequest(It.IsAny<int>());

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal($"Сущность с заданным Id: {request.Id} не найдена.", exception.Message);
        }
    }
}
