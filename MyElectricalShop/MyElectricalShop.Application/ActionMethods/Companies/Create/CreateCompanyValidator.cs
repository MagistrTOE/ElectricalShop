using FluentValidation;

namespace MyElectricalShop.Application.ActionMethods.Companies.Create
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Введите имя компании");

            RuleFor(x => x.Name)
                .MaximumLength(20)
                .WithMessage("Имя не должно содержать более 20 символов");
        }
    }
}
