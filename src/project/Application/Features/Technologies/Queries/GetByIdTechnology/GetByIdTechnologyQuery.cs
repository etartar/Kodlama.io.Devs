using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetByIdTechnology
{
    public class GetByIdTechnologyQuery : IRequest<TechnologyGetByIdDto>
    {
        public GetByIdTechnologyQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public GetByIdTechnologyQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.GetAsync(
                            predicate: x => x.Id == request.Id,
                            include: t => t.Include(x => x.ProgrammingLanguage));

                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                TechnologyGetByIdDto mappedTechnology = _mapper.Map<TechnologyGetByIdDto>(technology);

                return mappedTechnology;
            }
        }
    }
}
