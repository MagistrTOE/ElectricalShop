using MediatR;
using AutoMapper;
using MyElectricalShop.Infrastructure.Repositories;
using MyElectricalShop.Domain.Interfaces;
using CompanyEntity = MyElectricalShop.Domain.Models.Company;

namespace MyElectricalShop.Application.ActionMethods.Company.Delete
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyRequest,DeletedCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<DeletedCompanyResponse> Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetById(request.Id);
            await _companyRepository.Delete(company);

            //return Unit.Value;
            
            return _mapper.Map<DeletedCompanyResponse>(company);
        }
    }
}
