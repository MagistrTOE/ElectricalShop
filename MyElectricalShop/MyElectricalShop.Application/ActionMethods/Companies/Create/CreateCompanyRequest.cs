using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.Companies.Create
{
    public class CreateCompanyRequest : IRequest<CreatedCompanyResponse>
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class CreateCompanyHandler : IRequestHandler<CreateCompanyRequest, CreatedCompanyResponse>
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
            var company = _mapper.Map<Company>(request);
            await _companyRepository.Add(company);

            return _mapper.Map<CreatedCompanyResponse>(company);
        }
    }
}
