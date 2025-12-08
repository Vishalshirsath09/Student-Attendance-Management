using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;


namespace Kemar.SMS.Business.TeacherBusiness
{
    public interface ITeacherService
    {
        Task<ResultModel> AddOrUpdateAsync(TeacherRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(string? name, string? emial);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}

