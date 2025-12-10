using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Repository.Repositories.AttendanceRepo;


namespace Kemar.SMS.Business.AttendanceBusiness
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendance _repository;

        public AttendanceService(IAttendance repository)
        {
            _repository = repository;
        }

        public async Task<ResultModel> AddOrUpdateAsync(AttendanceRequest request)
        {
            // CreatedBy / UpdatedBy already set in Controller
            if (request.AttendanceId == 0)
            { 
            request.IsActive = true;
                request.CreatedBy = request.CreatedBy;
            request.CreatedAt = DateTime.UtcNow;
        }
            else
                request.UpdatedAt = DateTime.UtcNow;

            return await _repository.AddOrUpdateAsync(request);
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResultModel> GetByFilterAsync(int? studentId, int? subjectId, int? teacherId, DateTime? date)
        {
            return await _repository.GetByFilterAsync(studentId, subjectId,teacherId,date);
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }
    }
}
