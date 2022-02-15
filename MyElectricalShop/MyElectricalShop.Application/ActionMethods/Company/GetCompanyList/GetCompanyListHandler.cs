using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Company.GetCompanyList
{
    public class GetCompanyListHandler : IRequestHandler<GetCompanyListRequest, List<CompanyResponse>>
    {
        protected readonly IMapper _mapper;
        protected readonly ICompanyRepository _companyRepository;

        public GetCompanyListHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyResponse>> Handle(GetCompanyListRequest request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAll();
            
            return _mapper.Map<List<CompanyResponse>>(company);
        }
    }
}