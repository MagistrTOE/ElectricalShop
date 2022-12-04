using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Moq;

namespace MyElectricalShop.Identity.Tests.Common
{
    public static class MockTypes
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var userManager = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Object.UserValidators.Add(new UserValidator<TUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            return userManager;
        }

        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            return new Mock<SignInManager<TUser>>(
                MockUserManager<TUser>().Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<TUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<TUser>>().Object);
        }
    }
}
