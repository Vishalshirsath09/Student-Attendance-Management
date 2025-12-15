using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;

namespace Kemar.SMS.Business.AttendanceBusiness
{
    public interface IAttendanceService
    {
        Task<ResultModel> AddOrUpdateAsync(AttendanceRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(int? studentId, int? subjectId, int? teacherId, DateTime? date);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}
