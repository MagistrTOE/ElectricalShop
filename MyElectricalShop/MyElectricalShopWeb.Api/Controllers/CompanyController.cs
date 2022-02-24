using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Companies.Create;
using MyElectricalShop.Application.ActionMethods.Companies.GetList;
using MyElectricalShop.Application.ActionMethods.Companies.Delete;

namespace MyElectricalShop.Web.Api.Controllers
{
    [Route("Companies")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CreatedCompanyResponse> AddCompany([FromBody] CreateCompanyRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("list")]
        public async Task<List<CompanyResponseItem>> GetListCompany()
        {
            return await _mediator.Send(new GetCompanyListRequest());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _mediator.Send(new DeleteCompanyRequest(id));

            return NoContent();
        }
    }
}
