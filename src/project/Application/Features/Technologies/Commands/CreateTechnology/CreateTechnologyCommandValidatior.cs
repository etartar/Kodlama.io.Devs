using FluentValidation;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommandValidatior : AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyCommandValidatior()
        {
            RuleFor(x => x.ProgrammingLanguageId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
