using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Models;
using Profile = AutoMapper.Profile;

namespace JobAdvertisementAppAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Education, EducationDto>();
            CreateMap<EducationDto, Education>();
            CreateMap<JobExperience, JobExpirienceDto>();
            CreateMap<JobExpirienceDto, JobExperience>();
            CreateMap<JobLevel, JobLevelDto>();
            CreateMap<JobLevelDto, JobLevel>();
            CreateMap<JobType, JobTypeDto>();
            CreateMap<JobTypeDto, JobType>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();
            CreateMap<Profile, ProfileDto>();
            CreateMap<ProfileDto, Profile>();
            CreateMap<TypeOfContract, TypeOfContractDto>();
            CreateMap<TypeOfContractDto, TypeOfContract>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<WorkingShift, WorkingShiftDto>();
            CreateMap<WorkingShiftDto, WorkingShift>();
        }
    }
}
