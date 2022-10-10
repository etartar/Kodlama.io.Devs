using Application.Common.MediatR;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimByUserIdCommand : SecuredBaseCommand<DeletedUserOperationClaimDto>
    {
        public DeleteUserOperationClaimByUserIdCommand()
        {
            SetRoles(Admin, UserOperationClaimDelete);
        }

        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class DeleteUserOperationClaimByUserIdCommandHandler : IRequestHandler<DeleteUserOperationClaimByUserIdCommand, DeletedUserOperationClaimDto>
        {
            private readonly IMapper _mapper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public DeleteUserOperationClaimByUserIdCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _mapper = mapper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimByUserIdCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.CheckUserExist(request.UserId);

                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(x => x.UserId == request.UserId && x.OperationClaimId == request.OperationClaimId);

                _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenRequested(userOperationClaim);

                UserOperationClaim deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
                DeletedUserOperationClaimDto deletedUserOperationClaimDto = _mapper.Map<DeletedUserOperationClaimDto>(deletedUserOperationClaim);

                return deletedUserOperationClaimDto;
            }
        }
    }
}
