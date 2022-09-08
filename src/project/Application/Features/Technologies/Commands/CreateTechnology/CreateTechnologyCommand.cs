using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);

                Technology technology = _mapper.Map<Technology>(request);
                Technology createdTechnology = await _technologyRepository.AddAsync(technology);
                CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

                return createdTechnologyDto;
            }
        }
    }
}
