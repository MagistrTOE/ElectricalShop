using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Companies.Create;
using MyElectricalShop.Application.ActionMethods.Companies.Delete;
using MyElectricalShop.Application.ActionMethods.Companies.GetList;

namespace MyElectricalShop.Web.Api.Controllers
{
    [Route("companies")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
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
