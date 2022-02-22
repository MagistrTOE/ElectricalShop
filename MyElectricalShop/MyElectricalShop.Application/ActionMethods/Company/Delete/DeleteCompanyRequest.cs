using MediatR;

namespace MyElectricalShop.Application.ActionMethods.Company.Delete
{
    public class DeleteCompanyRequest : IRequest<DeletedCompanyResponse>
    {
        public int Id { get; set; }

        public DeleteCompanyRequest(int id)
        {
            Id = id;
        }
    }
}
