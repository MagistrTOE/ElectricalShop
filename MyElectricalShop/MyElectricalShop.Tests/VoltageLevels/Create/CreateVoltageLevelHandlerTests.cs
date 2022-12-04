using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.VoltageLevels;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Create;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.VoltageLevels.Create
{
    public class CreateVoltageLevelHandlerTests
    {
        private readonly Mock<IVoltageLevelRepository> _IVoltageLevelRepositoryMock;
        private readonly CreateVoltageLevelHandler _handler;

        public CreateVoltageLevelHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VoltageLevelMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _IVoltageLevelRepositoryMock = new Mock<IVoltageLevelRepository>();
            _handler = new CreateVoltageLevelHandler(_IVoltageLevelRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task CreateVoltageLevel_Success()
        {
            // Arrange
            _IVoltageLevelRepositoryMock.Setup(x => x.Add(It.IsAny<VoltageLevel>()));

            var request = new CreateVoltageLevelRequest { MinLevel = 220, MaxLevel = 380 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<CreatedVoltageLevelResponse>(result);
            Assert.Equal(request.MinLevel, request.MinLevel);
            _IVoltageLevelRepositoryMock.Verify(x => x.Add(It.IsAny<VoltageLevel>()), Times.AtMostOnce);
        }
    }
}
