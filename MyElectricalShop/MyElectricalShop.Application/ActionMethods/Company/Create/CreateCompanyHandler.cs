using AutoMapper;
using MediatR;
using CompanyEntity = MyElectricalShop.Domain.Models.Company;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Company.Create
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyRequest,CreatedCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyHandler(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CreatedCompanyResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = _mapper.Map<CompanyEntity>(request);
            await _companyRepository.Add(company);
            return _mapper.Map<CreatedCompanyResponse>(company);
        }
    }
}
