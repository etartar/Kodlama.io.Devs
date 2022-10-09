using Application.Features.Auths.Constants;
using Application.Features.Technologies.Constants;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public string[] Roles => new string[]
        {
            AuthRoleClaims.Admin,
            TechnologyClaims.Update
        };

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(request.Id, request.Name);

                Technology? technology = await _technologyRepository.GetAsync(x => x.Id == request.Id);

                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                technology.ProgrammingLanguageId = request.ProgrammingLanguageId;
                technology.Name = request.Name;

                Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
                UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

                return updatedTechnologyDto;
            }
        }
    }
}
