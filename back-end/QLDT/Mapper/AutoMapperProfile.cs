using AutoMapper;
using QLDT.Models;
using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // TrainingFormat
            CreateMap<TrainingFormat, TrainingFormatRes>();
            CreateMap<TrainingFormatReq, TrainingFormat>();

            // TrainingUnit
            CreateMap<TrainingUnit, TrainingUnitRes>();
            CreateMap<TrainingUnitReq, TrainingUnit>();

            // EducationLevel
            CreateMap<EducationLevel, EducationLevelRes>();
            CreateMap<EducationLevelReq, EducationLevel>();

            // Role
            CreateMap<Role, RoleRes>();
            CreateMap<RoleReq, Role>();

            // Part
            CreateMap<Part, PartRes>();
            CreateMap<PartReq, Part>();
        }
    }
}
