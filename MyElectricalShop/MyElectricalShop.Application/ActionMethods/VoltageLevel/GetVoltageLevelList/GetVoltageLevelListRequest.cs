using MediatR;
namespace MyElectricalShop.Application.ActionMethods.VoltageLevel.GetVoltageLevelList
{
    public class GetVoltageLevelListRequest : IRequest<List<VoltageLevelResponse>>
    {
    }
}
