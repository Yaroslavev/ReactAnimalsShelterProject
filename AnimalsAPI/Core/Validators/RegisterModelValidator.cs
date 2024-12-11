using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using FluentValidation;

namespace Core.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator() {
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Birthdate).LessThanOrEqualTo(DateTime.Now);
        }
    }
}
