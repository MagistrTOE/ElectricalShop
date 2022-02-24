using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Create;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.GetVoltageLevelList;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Delete;

namespace MyElectricalShop.Web.Api.Controllers
{   
    [Route("voltageLevels")]
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
        public async Task<List<VoltageLevelResponse>> GetListVoltageLevel()
        {
            return await _mediator.Send(new GetVoltageLevelListRequest());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVoltageLevel (int id)
        {
            await _mediator.Send(new DeleteVoltageLevelRequest(id));

            return NoContent();
        }
    }
}
