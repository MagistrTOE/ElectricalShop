using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyElectricalShop.Application.ActionMethods.Products.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Введите имя продукта");

            RuleFor(x => x.Name)
                .MaximumLength(20)
                .WithMessage("Имя не должно содержать более 20 символов");

            RuleFor(x => x.Power)
                .NotEmpty()
                .WithMessage("Укажите мощность");

            RuleFor(x => x.Power)
                .Must(x => x <= 15920)
                .WithMessage("Максимальная мощность 15920 кВт");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Укажите цену продукта");
    
            RuleFor(x => x.Price)
                .Must(x => x <= 999999999)
                .WithMessage("Максимальная цена 999 999 999 ₽");
        }
    }
}
