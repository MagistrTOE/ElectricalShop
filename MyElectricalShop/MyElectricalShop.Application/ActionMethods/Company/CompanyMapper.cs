﻿using AutoMapper;
using MyElectricalShop.Application.ActionMethods.Company.Create;
using MyElectricalShop.Application.ActionMethods.Company.GetCompanyList;
using Companies = MyElectricalShop.Domain.Models.Company;
using MyElectricalShop.Application.ActionMethods.Company.Delete;

namespace MyElectricalShop.Application.ActionMethods.Company
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<CreateCompanyRequest, Companies>();
            CreateMap<Companies, CreatedCompanyResponse>();
            CreateMap<Companies, CompanyResponse>();
            //CreateMap<Companies, DeleteCompanyRequest>();
            CreateMap<Companies, DeletedCompanyResponse>();
        }
    }
}
