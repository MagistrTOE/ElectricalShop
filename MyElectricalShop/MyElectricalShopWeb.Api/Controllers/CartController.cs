using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Carts.Create;
using MyElectricalShop.Application.ActionMethods.Carts.UpdateCartLine;
using MyElectricalShop.Application.ActionMethods.Carts.AddCartLine;
using MyElectricalShop.Application.ActionMethods.Carts.Clear;

namespace MyElectricalShop.Web.Api.Controllers
{
    [Route("carts")]
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

        [HttpPost("cartLine")]
        public async Task AddLineInCart([FromBody] AddCartLineRequest request)
        {
            await _mediator.Send(request);
        }

        [HttpPut("cartLine")]
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
    }
}
