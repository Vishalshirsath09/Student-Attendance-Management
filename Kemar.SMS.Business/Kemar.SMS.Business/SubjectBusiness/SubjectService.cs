using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Repositories.SubjectRepo;

namespace Kemar.SMS.Business.SubjectBusiness
{
    public class SubjectService : ISubjectService

    {
        private readonly ISubject _repository;

        public SubjectService(ISubject repository)
        {
            _repository = repository;
        }

        public async Task<SubjectResponse> CreateAsync(SubjectRequest request)
        {
            return await _repository.CreateAsync(request);
        }

        public async Task<IEnumerable<SubjectResponse>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<SubjectResponse?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<SubjectResponse?> UpdateAsync(int id, SubjectRequest request)
        {
            return await _repository.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

