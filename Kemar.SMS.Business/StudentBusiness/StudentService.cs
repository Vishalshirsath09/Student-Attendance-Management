using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
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

        public async Task<StudentResponse> CreateAsync(StudentRequest request)
        {
            return await _repository.CreateAsync(request);
        }

        public async Task<IEnumerable<StudentResponse>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<StudentResponse?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<StudentResponse?> UpdateAsync(int id, StudentRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
