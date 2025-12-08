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
            #region User
            // Map UserRequest → User (for creating/updating)
            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Id is DB generated
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // hashed manually
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            // Map User → UserResponse (for returning to client)
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            #endregion


            #region Student
            CreateMap<StudentRequest, Student>()
                .ForMember(dest => dest.StudentId, opt => opt.Ignore());
            CreateMap<Student, StudentResponse>();
            #endregion

            #region Teacher
            CreateMap<TeacherRequest, Teacher>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore());
            CreateMap<Teacher, TeacherResponse>();
            #endregion

            #region Subject
            CreateMap<SubjectRequest, Subject>()
                .ForMember(dest => dest.SubjectId, opt => opt.Ignore());
            CreateMap<Subject, SubjectResponse>();
            #endregion

            #region Attendance
            //CreateMap<AttendanceRequest, Attendance>()
            //    .ForMember(dest => dest.AttendanceId, opt => opt.Ignore());

            //CreateMap<Attendance, AttendanceResponse>()
            //    .ForMember(dest => dest.SubjectName,
            //        opt => opt.MapFrom(src => src.Subject!.SubjectName))
            //    .ForMember(dest => dest.TeacherName,
            //        opt => opt.MapFrom(src => src.Subject!.TeacherName));
            #endregion
        }
    }
}
