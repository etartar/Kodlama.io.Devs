using Application.Common.MediatR;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.UserOperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim
{
    public class GetListUserOperationClaimQuery : SecuredBaseQuery<List<UserOperationClaimListDto>>
    {
        public GetListUserOperationClaimQuery()
        {
            SetRoles(Admin, UserOperationClaimGetList);
        }

        public int UserId { get; set; }

        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, List<UserOperationClaimListDto>>
        {
            private readonly IMapper _mapper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _mapper = mapper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<List<UserOperationClaimListDto>> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.CheckUserExist(request.UserId);

                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                    x => x.UserId == request.UserId,
                    include: i => i.Include(x => x.OperationClaim),
                    index: 0,
                    size: 10000);

                List<UserOperationClaimListDto> userOperationClaimsList = _mapper.Map<List<UserOperationClaimListDto>>(userOperationClaims.Items);

                return userOperationClaimsList;
            }
        }
    }
}
