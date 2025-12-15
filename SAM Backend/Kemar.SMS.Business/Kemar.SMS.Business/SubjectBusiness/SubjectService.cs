using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
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

        public async Task<ResultModel> AddOrUpdateAsync(SubjectRequest request)
        {
            // CreatedBy / UpdatedBy already set in Controller
            if (request.SubjectId == 0)
                request.IsActive = true;
            else
                request.UpdatedAt = DateTime.UtcNow;

            return await _repository.AddOrUpdateAsync(request);
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResultModel> GetByFilterAsync(string? subjectName, string? subjectCode)
        {
            return await _repository.GetByFilterAsync(subjectName, subjectCode);
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }
    }
}
