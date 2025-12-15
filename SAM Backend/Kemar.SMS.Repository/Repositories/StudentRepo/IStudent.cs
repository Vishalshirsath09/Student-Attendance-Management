using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;

namespace Kemar.SMS.Repository.Repositories.StudentRepo
{
    public interface IStudent
    {
        Task<StudentResponse>CreateAsync(StudentRequest request);
        Task<IEnumerable<StudentResponse>>GetAllAsync();
        Task<StudentResponse?>GetByIdAsync(int id);
        Task<StudentResponse?>UpdateAsync(int id, StudentRequest request);
        Task<bool>DeleteAsync(int id);
    }
}
