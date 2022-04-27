using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Identity.Application.Users.Registration;
using MyElectricalShop.Shared.ExternalEvents;

namespace MyElectricalShop.Identity.Web.Api.Controllers
{
    [Route("users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : Controller
    {
        public readonly IMediator _mediator;
        public readonly IPublishEndpoint _publishEndpoint;

        public UserController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegistrationRequest request)
        {
            var createdUserId = await _mediator.Send(request);

            var createdCart = new CreatedCart { UserId = createdUserId };

            await _publishEndpoint.Publish(createdCart);

            return Ok();
        }
    }
}
