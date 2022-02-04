using Application.Data.DataObjects;
using Application.Data.ResponseData;
using Application.Service.Models;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Service.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Application, ApplicationDTO>().ReverseMap();

            CreateMap<Applicant, ApplicantDTO>().ReverseMap();
            CreateMap<RequestedCredit, RequestedCreditDTO>().ReverseMap();

            CreateMap<ApplicationResponse, ApplicationDTO>().ReverseMap();
            CreateMap<ApplicantResponse, ApplicantDTO>().ReverseMap();
            CreateMap<RequestedCreditResponse, RequestedCreditDTO>().ReverseMap();



        }
    }
}