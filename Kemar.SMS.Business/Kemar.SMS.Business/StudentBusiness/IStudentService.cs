using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;

namespace Kemar.SMS.Business.StudentBusiness
{
    public interface IStudentService
    {
        Task<ResultModel> AddOrUpdateAsync(StudentRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(string? name, string? className, string? div);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}
