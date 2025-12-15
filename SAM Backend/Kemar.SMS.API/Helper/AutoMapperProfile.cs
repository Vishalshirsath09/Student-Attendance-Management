using AutoMapper;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Entity;

namespace Kemar.SMS.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Student
            CreateMap<StudentRequest, Student>()
                .ForMember(dest => dest.StudentId, opt => opt.Ignore());
            CreateMap<Student, StudentResponse>();
            #endregion

            #region Teacher
            CreateMap<TeacherRequest, Teacher>();
            CreateMap<Teacher, TeacherResponse>();
            #endregion
        }
    }
}
