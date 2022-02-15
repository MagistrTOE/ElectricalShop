using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevel.GetVoltageLevelList
{
    public class GetVoltageLevelListHandler : IRequestHandler<GetVoltageLevelListRequest, List<VoltageLevelResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IVoltageLevelRepository _voltageLevelRepository;

        public GetVoltageLevelListHandler(IVoltageLevelRepository voltageLevelRepository, IMapper mapper)
        {
            _mapper = mapper;
            _voltageLevelRepository = voltageLevelRepository;
        }

        public async Task<List<VoltageLevelResponse>> Handle(GetVoltageLevelListRequest request, CancellationToken cancellationToken)
        {
            var voltageLevelList = await _voltageLevelRepository.GetAll();

            return _mapper.Map<List<VoltageLevelResponse>>(voltageLevelList);

        }
    }
}
