using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Repository.Repositories.StudentRepo;

namespace Kemar.SMS.Business.StudentBusiness
{
    public class StudentService : IStudentService
    {
        private readonly IStudent _repository;

        public StudentService(IStudent repository)
        {
            _repository = repository;
        }

        public async Task<ResultModel> AddOrUpdateAsync(StudentRequest request)
        {
            // Set audit fields (CreatedBy/UpdatedBy)
            if (request.StudentId == 0)
            {
                // New student
                request.CreatedAt = DateTime.UtcNow;
                request.IsActive = true;
            }
            else
            {
                // Existing student
                request.UpdatedAt = DateTime.UtcNow;
            }

            // Delegate to repository
            return await _repository.AddOrUpdateAsync(request);
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResultModel> GetByFilterAsync(string? name, string? className, string? div)
        {
            return await _repository.GetByFilterAsync(name, className, div);
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }
    }
}
