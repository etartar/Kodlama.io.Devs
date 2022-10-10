using Application.Common.MediatR;
using Application.Features.UserProfileLinks.Models;
using Application.Features.UserProfileLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.UserProfileLinks.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.UserProfileLinks.Queries.GetListByUserIdUserProfileLink
{
    public class GetListByUserIdUserProfileLinkQuery : SecuredBaseQuery<UserProfileLinkModel>
    {
        public GetListByUserIdUserProfileLinkQuery()
        {
            SetRoles(Admin, UserProfileLinkGetList);
        }

        public PageRequest PageRequest { get; set; }
        public int UserId { get; set; }

        public class GetListByUserIdUserProfileLinkQueryHandler : IRequestHandler<GetListByUserIdUserProfileLinkQuery, UserProfileLinkModel>
        {
            private readonly IMapper _mapper;
            private readonly IUserProfileLinkRepository _userProfileLinkRepository;
            private readonly UserProfileLinkBusinessRules _userProfileLinkBusinessRules;

            public GetListByUserIdUserProfileLinkQueryHandler(IMapper mapper, IUserProfileLinkRepository userProfileLinkRepository, UserProfileLinkBusinessRules userProfileLinkBusinessRules)
            {
                _mapper = mapper;
                _userProfileLinkRepository = userProfileLinkRepository;
                _userProfileLinkBusinessRules = userProfileLinkBusinessRules;
            }

            public async Task<UserProfileLinkModel> Handle(GetListByUserIdUserProfileLinkQuery request, CancellationToken cancellationToken)
            {
                await _userProfileLinkBusinessRules.CheckUserExist(request.UserId);

                IPaginate<UserProfileLink> userProfileLinks = await _userProfileLinkRepository.GetListAsync(x => x.UserId == request.UserId,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                UserProfileLinkModel mappedUserProfileLinkModel = _mapper.Map<UserProfileLinkModel>(userProfileLinks);

                return mappedUserProfileLinkModel;
            }
        }
    }
}
