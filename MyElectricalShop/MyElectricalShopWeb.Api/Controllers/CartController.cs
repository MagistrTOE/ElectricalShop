using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Carts.Create;
using MyElectricalShop.Application.ActionMethods.Carts.UpdateCartLine;
using MyElectricalShop.Application.ActionMethods.Carts.AddCartLine;
using MyElectricalShop.Application.ActionMethods.Carts.Clear;
using MyElectricalShop.Application.ActionMethods.Carts.GetPrice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyElectricalShop.Web.Api.Controllers
{
    [Route("carts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CreatedCartResponse> AddCart([FromBody] CreateCartRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("cart-line")]
        public async Task AddLineInCart([FromBody] AddCartLineRequest request)
        {
            await _mediator.Send(request);
        }

        [HttpPut("cart-line")]
        public async Task<UpdateCartLineResponse> UpdateLineInCart([FromBody] UpdateCartLineRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCartLine(Guid id)
        {
            await _mediator.Send(new ClearCartRequest(id));

            return NoContent();
        }

        [HttpGet("{userId:Guid}/price")]
        public async Task<GetPriceResponse> GetAllPriceByUserId (Guid userId)
        {
            return await _mediator.Send(new GetPriceRequest(userId));
        }
    }
}
