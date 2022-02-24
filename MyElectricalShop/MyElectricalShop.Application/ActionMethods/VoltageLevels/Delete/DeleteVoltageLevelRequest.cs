using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevels.Delete
{
    public class DeleteVoltageLevelRequest : IRequest
    {
        public int Id { get; }

    public DeleteVoltageLevelRequest(int id)
        {
            Id = id;
        }
    }

    public class DeleteVoltageLevelHandler : IRequestHandler<DeleteVoltageLevelRequest>
    {
        private readonly IVoltageLevelRepository _voltageLevelRepository;

        public DeleteVoltageLevelHandler(IVoltageLevelRepository voltageLevelRepository)
        {
            _voltageLevelRepository = voltageLevelRepository;
        }

        public async Task<Unit> Handle(DeleteVoltageLevelRequest request, CancellationToken cancellationToken)
        {
            var voltageLevel = await _voltageLevelRepository.GetById(request.Id);
            await _voltageLevelRepository.Delete(voltageLevel);

            return Unit.Value;
        }
    }
}
