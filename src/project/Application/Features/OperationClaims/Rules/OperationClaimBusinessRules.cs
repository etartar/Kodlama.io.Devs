using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<OperationClaim> result = await _operationClaimRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("OperationClaim name exists.");
        }

        public async Task OperationClaimNameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            IPaginate<OperationClaim> result = await _operationClaimRepository.GetListAsync(x => x.Id != id && x.Name == name);
            if (result.Items.Any()) throw new BusinessException("OperationClaim name exists.");
        }

        public void OperationClaimShouldExistWhenRequested(OperationClaim? operationClaim)
        {
            if (operationClaim == null) throw new BusinessException("Requested operation claim does not exist.");
        }
    }
}
