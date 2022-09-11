using Application.Features.Auth.Commands.AuthRegister;
using FluentValidation;

namespace Application.Features.Auth.Commands.AuthLogin
{
    public class AuthLoginCommandValidator : AbstractValidator<AuthRegisterCommand>
    {
        public AuthLoginCommandValidator()
        {
            RuleFor(l=>l.UserForRegisterDto.Email).NotEmpty();
            RuleFor(l=>l.UserForRegisterDto.Password).NotEmpty();
        }
    }
}
