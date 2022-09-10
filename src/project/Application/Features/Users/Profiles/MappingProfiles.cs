using Application.Features.Users.Commands.CreateUserProfileLink;
using Application.Features.Users.Commands.RegisterUser;
using Application.Features.Users.Commands.UpdateUserProfileLink;
using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Domain.Entities;
using Domain.Entities;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<User, CreatedUserDto>().ReverseMap();
            CreateMap<UserProfileLink, CreateUserProfileLinkCommand>().ReverseMap();
            CreateMap<UserProfileLink, CreatedUserProfileLinkDto>().ReverseMap();
            CreateMap<UserProfileLink, UpdateUserProfileLinkCommand>().ReverseMap();
            CreateMap<UserProfileLink, UpdatedUserProfileLinkDto>().ReverseMap();
            CreateMap<UserProfileLink, DeletedUserProfileLinkDto>().ReverseMap();
        }
    }
}
