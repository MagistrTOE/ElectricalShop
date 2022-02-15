using AutoMapper;
using MediatR;
using MyElectricalShop.Infrastructure.Data.Repositories;
using MyElectricalShop.Domain.Interfaces;
using VoltageLevelEntity = MyElectricalShop.Domain.Models.VoltageLevel;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevel.Create
{
    public class CreateVoltageLevelHandler : IRequestHandler<CreateVoltageLevelRequest,CreatedVoltageLevelResponse>
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
            var voltageLevel = _mapper.Map<VoltageLevelEntity>(request);
            await _voltageLevelRepository.Add(voltageLevel);

            return _mapper.Map<CreatedVoltageLevelResponse>(voltageLevel);
        }
    }
}
