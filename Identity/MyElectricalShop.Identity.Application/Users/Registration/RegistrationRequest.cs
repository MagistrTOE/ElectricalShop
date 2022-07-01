using Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyElectricalShop.Identity.Domain.Models;

namespace MyElectricalShop.Identity.Application.Users.Registration
{
    public class RegistrationRequest : IRequest<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class RegistrationHandler : IRequestHandler<RegistrationRequest, Guid>
    {
        private readonly UserManager<User> _userManager;

        public RegistrationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Guid> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors.Select(x => x.Description).ToArray());
            }

            return user.Id;
        }
    }
}
