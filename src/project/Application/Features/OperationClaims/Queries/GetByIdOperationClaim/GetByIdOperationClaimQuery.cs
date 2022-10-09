using Application.Features.Auths.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQuery : IRequest<OperationClaimGetByIdDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new string[]
        {
            AuthRoleClaims.Admin,
            OperationClaimClaims.GetById
        };

        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetByIdOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<OperationClaimGetByIdDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);

                _operationClaimBusinessRules.OperationClaimShouldExistWhenRequested(operationClaim);

                OperationClaimGetByIdDto mappedOperationClaim = _mapper.Map<OperationClaimGetByIdDto>(operationClaim);

                return mappedOperationClaim;
            }
        }
    }
}
