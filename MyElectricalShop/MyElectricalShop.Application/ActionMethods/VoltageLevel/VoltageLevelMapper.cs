using AutoMapper;
using MyElectricalShop.Application.ActionMethods.VoltageLevel.Create;
using MyElectricalShop.Application.ActionMethods.VoltageLevel.GetVoltageLevelList;
using VoltageLevelEntity = MyElectricalShop.Domain.Models.VoltageLevel;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevel
{
    public class VoltageLevelMapper : Profile
    {
        public VoltageLevelMapper()
        {
            CreateMap<CreateVoltageLevelRequest, VoltageLevelEntity>();
            CreateMap<VoltageLevelEntity, CreatedVoltageLevelResponse>();
            CreateMap<VoltageLevelEntity, VoltageLevelResponse>();
        }
    }
}
