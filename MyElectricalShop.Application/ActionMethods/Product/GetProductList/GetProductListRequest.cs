using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyElectricalShop.Application.ActionMethods.Product.GetProductList
{
    public class GetProductListRequest : IRequest<List<ProductResponse>>
    {

    }
}
