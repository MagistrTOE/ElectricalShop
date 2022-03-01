using FluentValidation;

namespace MyElectricalShop.Application.ActionMethods.VoltageLevels.Create
{
    public class CreateVoltageLevelValidator : AbstractValidator<CreateVoltageLevelRequest>
    {
        public CreateVoltageLevelValidator()
        {
            RuleFor(x => x.MinLevel)
                .Must(x => x >= 200 && x <= 11000)
                .WithMessage("Укажите значение минимального напряжения от 200 В до 11000 В");

            RuleFor(x => x.MaxLevel)
                .Must(x => x >= 230 && x <= 11000)
                .WithMessage("Укажите значение максимального напряжения от 230 до 11000 В");
        }
    }
}
