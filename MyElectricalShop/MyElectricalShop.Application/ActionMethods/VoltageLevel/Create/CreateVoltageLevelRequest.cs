using MediatR;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevel.Create
{
    public class CreateVoltageLevelRequest : IRequest<CreatedVoltageLevelResponse>
    {
        public string Level { get; set; }
    }
}
