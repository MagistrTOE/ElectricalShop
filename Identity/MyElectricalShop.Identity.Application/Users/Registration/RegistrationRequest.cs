using MediatR;
using Microsoft.AspNetCore.Identity;
using MyElectricalShop.Identity.Domain.Models;

namespace MyElectricalShop.Identity.Application.Users.Registration
{
    public class RegistrationRequest : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class RegistrationHandler : IRequestHandler<RegistrationRequest>
    {
        private readonly UserManager<User> _userManager;

        public RegistrationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName
            };

            await _userManager.CreateAsync(user, request.Password);
            return Unit.Value;
        }
    }
}
