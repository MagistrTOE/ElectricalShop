using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Product.GetProductList;
using MyElectricalShop.Application.ActionMethods.Product.AddProduct;

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

    [HttpPost("Create product")]
    public async Task<AddProductResponse> CreateProduct([FromBody] AddProductRequest request)
    {
        return await _mediator.Send(request);
    }
}
