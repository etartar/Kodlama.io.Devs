using Application.Features.Auths.Constants;
using Application.Features.Technologies.Constants;
using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetListTechnologyByDynamic
{
    public class GetListTechnologyByDynamicQuery : IRequest<TechnologyListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public string[] Roles => new string[]
        {
            AuthRoleClaims.Admin,
            TechnologyClaims.GetListByDynamic
        };

        public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechnologyListModel>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;

            public GetListTechnologyByDynamicQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> technologies = await _technologyRepository.GetListByDynamicAsync(
                            dynamic: request.Dynamic,
                            include: t => t.Include(x => x.ProgrammingLanguage),
                            index: request.PageRequest.Page,
                            size: request.PageRequest.PageSize);

                TechnologyListModel mappedTechnologyListModel = _mapper.Map<TechnologyListModel>(technologies);

                return mappedTechnologyListModel;
            }
        }
    }
}
