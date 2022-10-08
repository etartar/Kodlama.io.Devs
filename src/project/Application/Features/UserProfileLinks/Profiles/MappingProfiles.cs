using Application.Features.UserProfileLinks.Commands.CreateUserProfileLink;
using Application.Features.UserProfileLinks.Commands.UpdateUserProfileLink;
using Application.Features.UserProfileLinks.Dtos;
using Application.Features.UserProfileLinks.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.UserProfileLinks.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserProfileLink, UserProfileLinkListDto>()
                .ForMember(t => t.UserProfileLinkName, opt => opt.MapFrom(x => x.Name))
                .ReverseMap();
            CreateMap<IPaginate<UserProfileLink>, UserProfileLinkModel>().ReverseMap();
            CreateMap<UserProfileLink, CreateUserProfileLinkCommand>().ReverseMap();
            CreateMap<UserProfileLink, CreatedUserProfileLinkDto>().ReverseMap();
            CreateMap<UserProfileLink, UpdateUserProfileLinkCommand>().ReverseMap();
            CreateMap<UserProfileLink, UpdatedUserProfileLinkDto>().ReverseMap();
            CreateMap<UserProfileLink, DeletedUserProfileLinkDto>().ReverseMap();
        }
    }
}
