using Application.Models.Surveys;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers.Surveys
{
    public class SurveyMappingProfile : Profile
    {
        public SurveyMappingProfile() 
        {
            CreateMap<Survey, SurveyDto>();
        }
    }
}
