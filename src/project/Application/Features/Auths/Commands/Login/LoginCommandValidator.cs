using FluentValidation;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserForLoginDto).NotEmpty();
            RuleFor(x => x.UserForLoginDto.Email).NotEmpty();
            RuleFor(x => x.UserForLoginDto.Password).NotEmpty();
        }
    }
}
