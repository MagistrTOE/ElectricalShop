using AutoMapper;
using Moq;
using MyElectricalShop.Application.ActionMethods.VoltageLevels;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.GetVoltageLevelList;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Tests.VoltageLevels.GetList
{
    public class GetVoltageLevelListHandlerTests
    {
        private readonly Mock<IVoltageLevelRepository> _IVoltageLevelRepositoryMock;
        private readonly GetVoltageLevelListHandler _handler;

        public GetVoltageLevelListHandlerTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VoltageLevelMapper>());
            var mapper = new Mapper(mapperConfiguration);

            _IVoltageLevelRepositoryMock = new Mock<IVoltageLevelRepository>();
            _handler = new GetVoltageLevelListHandler(_IVoltageLevelRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetVoltageLevelList()
        {
            // Arrange
            _IVoltageLevelRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<VoltageLevel> {
                    new VoltageLevel { Id = It.IsAny<int>(), MinLevel = 200, MaxLevel = 380 }
                });

            var request = new GetVoltageLevelListRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<List<VoltageLevelResponse>>(result);

            var resultItem = result.First();
            Assert.Equal(200, resultItem.MinLevel);
            Assert.Equal(380, resultItem.MaxLevel);
        }
    }
}
