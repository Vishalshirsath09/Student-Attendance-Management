using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;

namespace Kemar.SMS.Business.AttendanceBusiness
{
    public interface IAttendanceService
    {
        Task<AttendanceResponse> CreateAsync(AttendanceRequest request);
        Task<IEnumerable<AttendanceResponse>> GetAllAsync();
        Task<AttendanceResponse?> GetByIdAsync(int id);
        Task<AttendanceResponse?> UpdateAsync(int id, AttendanceRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
