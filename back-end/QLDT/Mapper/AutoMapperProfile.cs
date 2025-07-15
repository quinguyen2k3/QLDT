using AutoMapper;
using QLDT.Models;
using QLDT.Dtos.response;
using QLDT.Dtos.request;
using QLDT.Dtos;

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

            // Part
            CreateMap<Part, PartRes>();
            CreateMap<PartReq, Part>();

            //Department
            CreateMap<Department, DepartmentRes>()
                .ForMember(dest => dest.partName, opt => opt.MapFrom(src => src.Part.Name))
                .ForMember(dest => dest.partId, opt => opt.MapFrom(src => src.Part.Id));

            CreateMap<DepartmentReq, Department>();

            //Course
            CreateMap<CourseReq, Course>();
            CreateMap<Course, CourseRes>()
                .ForMember(dest => dest.DepId, opt => opt.MapFrom(src => src.Department.Id))
                .ForMember(dest => dest.CourseNgayKg, opt => opt.MapFrom(src => src.CourseNgayKG ?? DateTime.MinValue))
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src =>
                    src.FileCourses != null
                    ? src.FileCourses.Select(fc => new FileDto
                    {   
                        Id = fc.Id,
                        FileName = fc.FileName,
                        FileUrl = fc.Path,
                    }).ToList()
                    : new List<FileDto>()));

            // Employee
            CreateMap<EmployeeReq, Employee>();
            CreateMap<Employee, EmployeeRes>()
                 .ForMember(dest => dest.DepId, opt => opt.MapFrom(src => src.Department.Id))
                 .ForMember(dest => dest.DepName, opt => opt.MapFrom(src => src.Department.Name))
                 .ForMember(dest => dest.LevelId, otp => otp.MapFrom(src => src.Level.Id))
                 .ForMember(dest => dest.LevelName, otp => otp.MapFrom(src => src.Level.Name));
            // User
            // CreateMap<User, UserRes>();
            // CreateMap<UserRes, User>();

            // Course
            // CreateMap<Course, CourseRes>();
            // CreateMap<CourseRes, Course>();

            // Tiếp tục khai báo tất cả mappers cần dùng trong QLDT tại đây.
        }
    }
}
