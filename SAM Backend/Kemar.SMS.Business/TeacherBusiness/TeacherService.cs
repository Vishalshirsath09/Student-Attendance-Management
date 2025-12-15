using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Repositories.TeacherRepo;

namespace Kemar.SMS.Business.TeacherBusiness
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacher _repository;

        public TeacherService(ITeacher repository)
        {
            _repository = repository;
        }

        public async Task<TeacherResponse> CreateAsync(TeacherRequest request)
        {
            return await _repository.CreateAsync(request);
        }

        public async Task<IEnumerable<TeacherResponse>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<TeacherResponse?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TeacherResponse?> UpdateAsync(int id, TeacherRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
