using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerLoginValidator : AbstractValidator<CustomerLoginDto>
    {
        public CustomerLoginValidator()
        {
            RuleFor(u => u.Email).NotNull().NotEmpty().WithMessage("Mail alanı boş geçilemez");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir e-Mail adresi giriniz! ");

            RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("Parola alanı boş geçilemez!");
            RuleFor(u => u.Password).Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir!");
        }
    }
}
