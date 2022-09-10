using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUserProfileLink
{
    public class UpdateUserProfileLinkCommandValidator : AbstractValidator<UpdateUserProfileLinkCommand>
    {
        public UpdateUserProfileLinkCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Link).NotEmpty().NotNull();
        }
    }
}
