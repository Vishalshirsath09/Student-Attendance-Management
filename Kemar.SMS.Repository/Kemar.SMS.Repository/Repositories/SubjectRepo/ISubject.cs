using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;

namespace Kemar.SMS.Repository.Repositories.SubjectRepo
{
    public interface ISubject
    {
        Task<SubjectResponse> CreateAsync(SubjectRequest request);
        Task<IEnumerable<SubjectResponse>> GetAllAsync();
        Task<SubjectResponse?> GetByIdAsync(int id);
        Task<SubjectResponse?> UpdateAsync(int id, SubjectRequest request);
        Task<bool> DeleteAsync(int id);
    }

}
