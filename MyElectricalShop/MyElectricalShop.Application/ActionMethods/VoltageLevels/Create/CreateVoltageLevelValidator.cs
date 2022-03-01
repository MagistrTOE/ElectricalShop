using FluentValidation;
using System.Text.RegularExpressions;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevels.Create
{
    public class CreateVoltageLevelValidator : AbstractValidator<CreateVoltageLevelRequest>
    {
        public CreateVoltageLevelValidator()
        {
            RuleFor(x => x.Level)
                .NotEmpty()
                .WithMessage("Укажите диапазон напряжения");

            RuleFor(x => x.Level)
                .Matches(@"[0-9]{3}-[0-9]{3}")
                .WithMessage("Формат вводимых данных: minValue - maxValue");
        }


    }


}
