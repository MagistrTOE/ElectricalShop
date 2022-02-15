using MediatR;
namespace MyElectricalShop.Application.ActionMethods.Company.GetCompanyList
{
    public class GetCompanyListRequest : IRequest<List<CompanyResponse>> 
    {
    }
}
