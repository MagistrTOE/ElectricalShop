using AutoMapper;
using MyElectricalShop.Application.ActionMethods.Companies.Create;
using MyElectricalShop.Application.ActionMethods.Companies.GetList;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.Companies
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<CreateCompanyRequest, Company>();
            CreateMap<Company, CreatedCompanyResponse>();
            CreateMap<Company, CompanyResponseItem>();
        }
    }
}
