using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Auth.Commands.AuthRegister
{
    public class AuthRegisterCommandValidator:AbstractValidator<AuthRegisterCommand>
    {
        public AuthRegisterCommandValidator()
        {
            RuleFor(r=>r.UserForRegisterDto.Email).NotEmpty();
            RuleFor(r=>r.UserForRegisterDto.Password).NotEmpty();
            RuleFor(r=>r.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(r=>r.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(r=>r.UserForRegisterDto.Password).MinimumLength(8);
            RuleFor(r=>r.UserForRegisterDto.FirstName).MinimumLength(2);
            RuleFor(r=>r.UserForRegisterDto.LastName).MinimumLength(2);
            RuleFor(r => r.UserForRegisterDto.Email).NotNull().EmailAddress();
        }

    }
}
