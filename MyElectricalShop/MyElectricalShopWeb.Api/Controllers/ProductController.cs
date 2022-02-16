using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Product.Create;
using MyElectricalShop.Application.ActionMethods.Product.GetProductList;

namespace MyElectricalShop.Controllers;

[ApiController]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getListProduct")]
    public async Task<List<ProductResponse>> GetListProductsWithFullInfo()
    {
        return await _mediator.Send(new GetProductListRequest());
    }

    [HttpPost("createProduct")]
    public async Task<CreatedProductResponse> CreateProduct([FromBody] CreateProductRequest request)
    {
        return await _mediator.Send(request);
    }
}
