using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly ITechnologyRepository _technologyRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository, ITechnologyRepository technologyRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _technologyRepository = technologyRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programming Language name exists.");
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p => p.Id != id && p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programming Language name exists.");
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage? programmingLanguage)
        {
            if (programmingLanguage == null) throw new BusinessException("Requested programming language does not exist.");
        }

        public async Task IFProgrammingLanguageHasTechnologiesCanNotBeDeletedWhenDeleted(int id)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(t => t.ProgrammingLanguageId == id);
            if (result.Items.Any()) throw new BusinessException("Programming Language cannot be delete. Because it has technologies.");
        }
    }
}
