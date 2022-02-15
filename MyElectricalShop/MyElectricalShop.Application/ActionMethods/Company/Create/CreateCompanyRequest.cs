using MediatR;

namespace MyElectricalShop.Application.ActionMethods.Company.Create
{
    public class CreateCompanyRequest : IRequest<CreatedCompanyResponse>
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
