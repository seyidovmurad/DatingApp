using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, 
                opt => opt.MapFrom(u => u.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(u => u.DateOfBirth.GetYearFromUtcNow()));
            CreateMap<Photo, PhotoDto>();
        }
    }
}