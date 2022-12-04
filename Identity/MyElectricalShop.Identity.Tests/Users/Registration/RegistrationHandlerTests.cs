using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Moq;
using MyElectricalShop.Identity.Application.Users.Registration;
using MyElectricalShop.Identity.Domain.Models;
using MyElectricalShop.Identity.Tests.Common;

namespace MyElectricalShop.Identity.Tests.Users.Registration
{
    public class RegistrationHandlerTests
    {
        private readonly Mock<UserManager<User>> _userManager;
        private readonly RegistrationHandler _handler;

        public RegistrationHandlerTests()
        {
            _userManager = MockTypes.MockUserManager<User>();
            _handler = new RegistrationHandler(_userManager.Object);
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            // Arrange 
            _userManager
                .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var request = new RegistrationRequest() { UserName = "TestUserName", Password = "TestPassword" };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Guid>(result);
            _userManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task CreateUser_Error()
        {
            // Arrange 
            _userManager
                .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "TestErrors" }));

            var request = new RegistrationRequest() { UserName = "TestUserName", Password = "TestPassword" };

            // Act
            var exception = await Assert.ThrowsAsync<IdentityException>(async () => await _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.Equal("TestErrors", exception.Message);
        }
    }
}
