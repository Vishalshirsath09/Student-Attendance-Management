using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;

namespace Kemar.SMS.Repository.Repositories.TeacherRepo
{
    public interface ITeacher
    {
        Task<ResultModel>AddOrUpdateAsync(TeacherRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(string? name, string? emial);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}
