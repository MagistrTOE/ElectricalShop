using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Company.Create;
using MyElectricalShop.Application.ActionMethods.Company.GetCompanyList;

namespace MyElectricalShop.Web.Api.Controllers
{
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddCompany")]
        public async Task<CreatedCompanyResponse> AddCompany([FromBody] CreateCompanyRequest request)
        {
            return await _mediator.Send(request);
        }

        [Route("getListCompany")]
        [HttpGet]
        public async Task<List<CompanyResponse>> GetListCompany()
        {
            return await _mediator.Send(new GetCompanyListRequest());
        }
    }
}
