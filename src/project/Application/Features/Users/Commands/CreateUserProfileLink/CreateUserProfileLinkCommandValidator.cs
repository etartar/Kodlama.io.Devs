﻿using FluentValidation;

namespace Application.Features.Users.Commands.CreateUserProfileLink
{
    public class CreateUserProfileLinkCommandValidator : AbstractValidator<CreateUserProfileLinkCommand>
    {
        public CreateUserProfileLinkCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Link).NotEmpty().NotNull();
        }
    }
}
