using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyByIdCommand : IRequest<DeletedTechnologyDto>
    {
        public DeleteTechnologyByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class DeleteTechnologyByIdCommandHandler : IRequestHandler<DeleteTechnologyByIdCommand, DeletedTechnologyDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public DeleteTechnologyByIdCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyByIdCommand request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.GetAsync(x => x.Id == request.Id);

                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
                DeletedTechnologyDto deletedTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);

                return deletedTechnologyDto;
            }
        }
    }
}
