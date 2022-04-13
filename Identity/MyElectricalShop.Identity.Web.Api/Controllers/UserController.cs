using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Identity.Application.Users.Registration;

namespace MyElectricalShop.Identity.Web.Api.Controllers
{
    [Route("users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : Controller
    {
        public readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegistrationRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
    }
}
