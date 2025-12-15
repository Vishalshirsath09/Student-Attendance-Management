using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;

namespace Kemar.SMS.Repository.Repositories.StudentRepo
{
    public interface IStudent
    {
        Task<ResultModel> AddOrUpdateAsync(StudentRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(string? name, string? className, string? div);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}
