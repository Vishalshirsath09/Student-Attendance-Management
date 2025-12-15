using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;

namespace Kemar.SMS.Repository.Repositories.UserRepo
{
    public interface IUser
    {
        Task<ResultModel> AddOrUpdateAsync(UserRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(string? username, string? role);
        Task<ResultModel> DeleteByIdAsync(int id);
        Task<ResultModel> ValidateUserAsync(string username, string password);
    }
}
