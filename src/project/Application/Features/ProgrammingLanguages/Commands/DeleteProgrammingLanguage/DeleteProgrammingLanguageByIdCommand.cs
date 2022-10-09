using Application.Features.Auths.Constants;
using Application.Features.ProgrammingLanguages.Constants;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageByIdCommand : IRequest<DeletedProgrammingLanguageDto>, ISecuredRequest
    {
        public DeleteProgrammingLanguageByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string[] Roles => new string[]
        {
            AuthRoleClaims.Admin,
            ProgrammingLanguageClaims.Delete
        };

        public class DeleteProgrammingLanguageByIdCommandHandler : IRequestHandler<DeleteProgrammingLanguageByIdCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public DeleteProgrammingLanguageByIdCommandHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageByIdCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules.IFProgrammingLanguageHasTechnologiesCanNotBeDeletedWhenDeleted(request.Id);

                ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id);

                _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

                ProgrammingLanguage deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(programmingLanguage);
                DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

                return deletedProgrammingLanguageDto;
            }
        }
    }
}
