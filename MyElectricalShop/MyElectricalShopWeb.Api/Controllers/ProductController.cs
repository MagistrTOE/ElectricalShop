using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyElectricalShop.Application.ActionMethods.Products.Create;
using MyElectricalShop.Application.ActionMethods.Products.GetProductList;
using MyElectricalShop.Application.ActionMethods.Products.GetProductById;
using MyElectricalShop.Application.ActionMethods.Products.Delete;
using Microsoft.AspNetCore.Authorization;

namespace MyElectricalShop.Controllers;

[Route("products")]
[ApiController]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    [Authorize]
    public async Task<List<ProductResponse>> GetListProductsWithFullInfo()
    {
        return await _mediator.Send(new GetProductListRequest());
    }

    [HttpPost]
    public async Task<CreatedProductResponse> CreateProduct([FromBody] CreateProductRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ProductResponse> GetProductById(Guid id)
    {
        return await _mediator.Send(new GetProductByIdRequest(id));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _mediator.Send(new DeleteProductRequest(id));

        return NoContent();
    }
}
