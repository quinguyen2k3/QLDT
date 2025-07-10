using AutoMapper;
using QLDT.Models;
using QLDT.Dtos.response;
using QLDT.Dtos.request;

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
            CreateMap<Department, DepartmentRes>();
            CreateMap<DepartmentReq, Department>();
            // User
            // CreateMap<User, UserRes>();
            // CreateMap<UserRes, User>();

            // Course
            // CreateMap<Course, CourseRes>();
            // CreateMap<CourseRes, Course>();

            // Employee
            // CreateMap<Employee, EmployeeRes>();
            // CreateMap<EmployeeRes, Employee>();

            // Tiếp tục khai báo tất cả mappers cần dùng trong QLDT tại đây.
        }
    }
}
