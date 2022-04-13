using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Identity.Application.Authentication.Login;
using MyElectricalShop.Identity.Web.Api.ViewModels;

namespace MyElectricalShop.Identity.Web.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthenticationsController : Controller
    {
        private readonly IIdentityServerInteractionService _identityInteractionService;
        private readonly IMediator _mediator;

        public AuthenticationsController(IIdentityServerInteractionService identityInteractionService, IMediator mediator)
        {
            _identityInteractionService = identityInteractionService;
            _mediator = mediator;
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            return View("~/Views/Error.cshtml");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string returnUrl)
        {
            var context = await _identityInteractionService.GetAuthorizationContextAsync(returnUrl);

            if (context == null)
            {
                return LocalRedirect("/error");
            }

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View("~/Views/Login.cshtml", loginViewModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest, [FromQuery] string returnUrl)
        {
            var authContext = await _identityInteractionService.GetAuthorizationContextAsync(returnUrl);

            if (authContext == null)
            {
                return LocalRedirect("/error");
            }

            try
            {

                var result = await _mediator.Send(loginRequest);

                if (!result._Success)
                {
                    var loginViewModel = new LoginViewModel
                    {
                        Login = loginRequest.Login,
                        Error = result._Error,
                        ReturnUrl = returnUrl,
                    };

                    return View("~/Views/Login.cshtml", loginViewModel);
                }
            }
            catch (Exception ex)
            {
                return LocalRedirect("/error");
            }

            return Redirect(returnUrl);
        }
    }
}