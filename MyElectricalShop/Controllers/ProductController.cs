using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Product.GetProductList;
using MyElectricalShop.Application.ActionMethods.Product.Create;
using System.Threading.Tasks;
using System.Collections.Generic;

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

    [HttpPost("create")]
    public async Task<CreatedProductResponse> CreateProduct([FromBody] CreateProductRequest request)
    {
        return await _mediator.Send(request);
    }
}
