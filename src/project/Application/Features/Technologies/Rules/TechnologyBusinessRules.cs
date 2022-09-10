using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists.");
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(x => x.Id != id && x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists.");
        }

        public void TechnologyShouldExistWhenRequested(Technology? technology)
        {
            if (technology == null) throw new BusinessException("Requested technology does not exist.");
        }
    }
}
