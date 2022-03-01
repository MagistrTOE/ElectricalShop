using AutoMapper;
using MediatR;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevels.Create
{
    public class CreateVoltageLevelRequest : IRequest<CreatedVoltageLevelResponse>
    {
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
    }

    public class CreateVoltageLevelHandler : IRequestHandler<CreateVoltageLevelRequest, CreatedVoltageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVoltageLevelRepository _voltageLevelRepository;

        public CreateVoltageLevelHandler(IVoltageLevelRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _voltageLevelRepository = repository;
        }

        public async Task<CreatedVoltageLevelResponse> Handle(CreateVoltageLevelRequest request, CancellationToken cancellationToken)
        {
            var voltageLevel = _mapper.Map<VoltageLevel>(request);
            await _voltageLevelRepository.Add(voltageLevel);

            return _mapper.Map<CreatedVoltageLevelResponse>(voltageLevel);
        }
    }
}
