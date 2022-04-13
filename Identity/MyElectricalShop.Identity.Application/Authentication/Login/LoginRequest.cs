using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyElectricalShop.Identity.Domain.Models;

namespace MyElectricalShop.Identity.Application.Authentication.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);

            if (user == null)
            {
                return LoginResponse.Error("Неверный логин или пароль");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return LoginResponse.Error("Неверный логин или пароль");
            };

            var identityUser = new IdentityServerUser(user.Id.ToString());
            await _httpContextAccessor.HttpContext.SignInAsync(identityUser);

            return LoginResponse.Success();
        }
    }

}
