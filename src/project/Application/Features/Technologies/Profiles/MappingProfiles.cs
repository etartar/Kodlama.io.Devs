using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, TechnologyListDto>()
                .ForMember(t => t.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))
                .ForMember(t => t.TechnologyName, opt => opt.MapFrom(x => x.Name))
                .ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();
            CreateMap<Technology, TechnologyGetByIdDto>()
                .ForMember(t => t.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))
                .ForMember(t => t.TechnologyName, opt => opt.MapFrom(x => x.Name))
                .ReverseMap();
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
        }
    }
}
