using AutoMapper;
using QLDT.Models;
using QLDT.Dtos.response;
using QLDT.Dtos.request;
using QLDT.Dtos;
using QLDT.Service;

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

            //Class
            CreateMap<ClassReq, Class>();
            CreateMap<Class, ClassRes>()
             .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Course.Id))
             .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id))
             .ForMember(dest => dest.FormatId, opt => opt.MapFrom(src => src.Format.Id))
             .ForMember(dest => dest.LevelId, opt => opt.MapFrom(src => src.Level.Id))
             .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src =>
                 src.FileClasses != null
                     ? src.FileClasses.Select(fc => new FileDto
                     {
                         Id = fc.Id,
                         FileName = fc.FileName,
                         FileUrl = fc.Path
                     }).ToList()
                     : new List<FileDto>()))
             .ForMember(dest => dest.EmployeeIds, opt => opt.MapFrom(src =>
                 src.Details != null
                     ? src.Details.Select(d => d.EmpId).Distinct().ToList()
                     : new List<long>()))
             .ForMember(dest => dest.SoTinhChi, opt => opt.MapFrom(src =>
                    src.Details != null && src.Details.Any()
                        ? src.Details.First().SoTinhChi
                        : 0));
            //Role
            CreateMap<Role, RoleRes>();

            //User
            CreateMap<UserReq, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<User,  UserRes>()
                .ForMember(dest => dest.RoleName, otp => otp.MapFrom(src => src.Role.Name));
            // Tiếp tục khai báo tất cả mappers cần dùng trong QLDT tại đây.
        }
    }
}
