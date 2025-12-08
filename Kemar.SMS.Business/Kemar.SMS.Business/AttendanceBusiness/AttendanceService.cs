using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
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

        public async Task<AttendanceResponse> CreateAsync(AttendanceRequest request)
        {
            return await _repository.CreateAsync(request);
        }

        public async Task<IEnumerable<AttendanceResponse>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AttendanceResponse?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<AttendanceResponse?> UpdateAsync(int id, AttendanceRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
