using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyElectricalShop.Application.ActionMethods.VoltageLevel.Create;
using MyElectricalShop.Application.ActionMethods.VoltageLevel.GetVoltageLevelList;

namespace MyElectricalShop.Web.Api.Controllers
{   
    [ApiController]
    public class VoltageLevelController : Controller
    {
        private readonly IMediator _mediator;

        public VoltageLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("addVoltageLevel")]
        public async Task<CreatedVoltageLevelResponse> AddVoltageLevel([FromBody] CreateVoltageLevelRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("getListVoltageLevel")]
        public async Task<List<VoltageLevelResponse>> GetListVoltageLevel()
        {
            return await _mediator.Send(new GetVoltageLevelListRequest());
        }
    }
}
