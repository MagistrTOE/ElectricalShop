using AutoMapper;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.Create;
using MyElectricalShop.Application.ActionMethods.VoltageLevels.GetVoltageLevelList;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevels
{
    public class VoltageLevelMapper : Profile
    {
        public VoltageLevelMapper()
        {
            CreateMap<CreateVoltageLevelRequest, VoltageLevel>();
            CreateMap<VoltageLevel, CreatedVoltageLevelResponse>();
            CreateMap<VoltageLevel, VoltageLevelResponse>();
        }
    }
}
