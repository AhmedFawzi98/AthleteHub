using AthleteHub.Application.Users.Authenticaion.RegisterUser;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Application.Users.UpdateUser;
using AthleteHub.Domain.Entities;
using AutoMapper;

namespace AthleteHub.Application.Users.MappingProfile;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserCommand, ApplicationUser>();
        CreateMap<ApplicationUser, UserDto>();

        CreateMap<UpdateUserCommand, ApplicationUser>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMemeber) => srcMemeber != null));

        CreateMap<ApplicationUser, UpdatedUserDto>();

    }
}
