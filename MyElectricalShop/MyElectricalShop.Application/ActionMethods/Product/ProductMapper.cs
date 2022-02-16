using AutoMapper;
using MyElectricalShop.Application.ActionMethods.Product.Create;
using MyElectricalShop.Application.ActionMethods.Product.GetProductList;
using Products = MyElectricalShop.Domain.Models.Product;
using CategoryEntity = MyElectricalShop.Domain.Models.Category;
using CompanyEntity = MyElectricalShop.Domain.Models.Company;
using VoltageLevelEntity = MyElectricalShop.Domain.Models.VoltageLevel;

namespace MyElectricalShop.Application.ActionMethods.Product
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Products, ProductResponse>();
            CreateMap<CreateProductRequest, Products>();
            CreateMap<Products, CreatedProductResponse>();
            CreateMap<CategoryEntity, CategoryResponseForProduct>();
            CreateMap<CompanyEntity, CompanyResponseForProduct>();
            CreateMap<VoltageLevelEntity, VoltageLevelResponseForProduct>();
        }
    }
}
