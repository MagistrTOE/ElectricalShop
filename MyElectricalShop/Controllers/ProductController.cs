using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Product.GetProductList;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace MyElectricalShop.Controllers;

[ApiController]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Route("shop")]
    [HttpGet]
    public async Task<List<ProductResponse>> GetListProduct()
    {
        return await _mediator.Send(new GetProductListRequest());
    }
}
