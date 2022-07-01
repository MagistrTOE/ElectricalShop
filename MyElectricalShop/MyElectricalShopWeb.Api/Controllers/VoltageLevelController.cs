using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Create;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Delete;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.GetVoltageLevelList;

namespace MyElectricalShop.Web.Api.Controllers
{
    [Route("voltage-levels")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class VoltageLevelController : Controller
    {
        private readonly IMediator _mediator;

        public VoltageLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CreatedVoltageLevelResponse> AddVoltageLevel([FromBody] CreateVoltageLevelRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<List<VoltageLevelResponse>> GetListVoltageLevel()
        {
            return await _mediator.Send(new GetVoltageLevelListRequest());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVoltageLevel(int id)
        {
            await _mediator.Send(new DeleteVoltageLevelRequest(id));

            return NoContent();
        }
    }
}
