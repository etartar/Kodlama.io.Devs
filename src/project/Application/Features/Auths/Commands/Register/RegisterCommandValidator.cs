using FluentValidation;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.IpAddress).NotEmpty();
            RuleFor(x => x.UserForRegisterDto).NotNull();
            RuleFor(x => x.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Email).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Password).NotEmpty();
        }
    }
}
