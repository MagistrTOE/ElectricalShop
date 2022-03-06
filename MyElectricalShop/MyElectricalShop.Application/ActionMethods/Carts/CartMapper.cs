using AutoMapper;
using MyElectricalShop.Application.ActionMethods.Carts.Create;
using MyElectricalShop.Application.ActionMethods.Carts.AddCartLine;
using MyElectricalShop.Application.ActionMethods.Carts.UpdateCartLine;
using MyElectricalShop.Domain.Models;


namespace MyElectricalShop.Application.ActionMethods.Carts
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            CreateMap<CreateCartRequest, Cart>();
            CreateMap<Cart, CreatedCartResponse>();
            CreateMap<AddCartLineRequest, CartLine>();
            CreateMap<CartLine, UpdateCartLineResponse>();
        }
    }
}
