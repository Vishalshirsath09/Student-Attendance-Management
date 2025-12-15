using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;

namespace Kemar.SMS.Business.SubjectBusiness
{
    public interface ISubjectService
    {
        Task<ResultModel> AddOrUpdateAsync(SubjectRequest request);
        Task<ResultModel> GetByIdAsync(int id);
        Task<ResultModel> GetByFilterAsync(string? subjectName, string? subjectCode);
        Task<ResultModel> DeleteByIdAsync(int id);
    }
}
