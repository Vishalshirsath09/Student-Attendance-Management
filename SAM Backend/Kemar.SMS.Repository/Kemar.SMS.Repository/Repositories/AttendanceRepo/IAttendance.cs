using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;

namespace Kemar.SMS.Repository.Repositories.AttendanceRepo
{
    public interface IAttendance
    {
        Task<ResultModel> AddOrUpdateAsync(AttendanceRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(int? studentId, int? subjectId, int? teacherId, DateTime? date);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}
