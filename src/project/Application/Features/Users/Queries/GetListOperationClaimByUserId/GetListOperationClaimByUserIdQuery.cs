using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetListOperationClaimByUserId
{
    public class GetListOperationClaimByUserIdQuery : IRequest<IList<OperationClaim>>
    {
        public GetListOperationClaimByUserIdQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }

        public class GetListOperationClaimByUserIdQueryHandler : IRequestHandler<GetListOperationClaimByUserIdQuery, IList<OperationClaim>>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public GetListOperationClaimByUserIdQueryHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<IList<OperationClaim>> Handle(GetListOperationClaimByUserIdQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(x => x.Id == request.UserId,
                                include: u => u.Include(uoc => uoc.UserOperationClaims).ThenInclude(oc => oc.OperationClaim));

                _userBusinessRules.CheckUserExist(user);

                return user.UserOperationClaims.Select(s => s.OperationClaim).ToList();
            }
        }
    }
}
