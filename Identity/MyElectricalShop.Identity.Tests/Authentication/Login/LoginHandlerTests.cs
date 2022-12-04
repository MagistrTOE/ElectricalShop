using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using MyElectricalShop.Identity.Application.Authentication.Login;
using MyElectricalShop.Identity.Domain.Models;
using MyElectricalShop.Identity.Tests.Common;

namespace MyElectricalShop.Identity.Tests.Authentication.Login
{
    public class LoginHandlerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly LoginHandler _handler;

        public LoginHandlerTests()
        {
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUserManager = MockTypes.MockUserManager<User>();
            _mockSignInManager = MockTypes.MockSignInManager<User>();
            _handler = new LoginHandler(_httpContextAccessor.Object, _mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task Login_Success()
        {
            // Arrange
            _mockUserManager
                .Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { UserName = "TestName"});

            _mockSignInManager
                .Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false))
                .ReturnsAsync(SignInResult.Success);

            _httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext());

            _httpContextAccessor.Setup(x => x.HttpContext.SignInAsync(It.IsAny<IdentityServerUser>()))
                .Returns(Task.CompletedTask);


            var request = new LoginRequest { Login = "TestLogin", Password = "TestPassword" };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<LoginResponse>(result);
        }
    }
}
