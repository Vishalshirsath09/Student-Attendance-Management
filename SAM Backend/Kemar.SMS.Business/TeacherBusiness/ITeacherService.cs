using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;

namespace Kemar.SMS.Business.TeacherBusiness
{
    public interface ITeacherService
    {
        Task<TeacherResponse> CreateAsync(TeacherRequest request);
        Task<IEnumerable<TeacherResponse>> GetAllAsync();
        Task<TeacherResponse?> GetByIdAsync(int id);
        Task<TeacherResponse?> UpdateAsync(int id, TeacherRequest request);
        Task<bool> DeleteAsync(int id);
    }
}

