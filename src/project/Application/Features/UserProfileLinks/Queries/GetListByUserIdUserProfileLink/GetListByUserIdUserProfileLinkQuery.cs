﻿using Application.Features.Auths.Constants;
using Application.Features.UserProfileLinks.Constants;
using Application.Features.UserProfileLinks.Models;
using Application.Features.UserProfileLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserProfileLinks.Queries.GetListByUserIdUserProfileLink
{
    public class GetListByUserIdUserProfileLinkQuery : IRequest<UserProfileLinkModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public int UserId { get; set; }

        public string[] Roles => new string[]
        {
            AuthRoleClaims.Admin,
            UserProfileLinkClaims.GetListByUserId
        };

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
