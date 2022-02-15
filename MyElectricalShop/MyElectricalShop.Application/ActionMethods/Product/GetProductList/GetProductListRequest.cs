using MediatR;

namespace MyElectricalShop.Application.ActionMethods.Product.GetProductList
{
    public class GetProductListRequest : IRequest<List<ProductResponse>>
    {
    }
}
