using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Repositories.TeacherRepo;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Business.TeacherBusiness
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacher _repository;
        private readonly KemarDBContext _context;

        public TeacherService(ITeacher repository, KemarDBContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<ResultModel> AddOrUpdateAsync(TeacherRequest request)
        {
            // Fetch the Username from User table using UserId
            var user = await _context.Users
                        .Where(x => x.UserId == request.UserId && x.IsActive)
                        .Select(x => x.Username)
                        .FirstOrDefaultAsync();

            if (user == null)
                return ResultModel.NotFound("Invalid UserId");

            if (request.TeacherId == 0)
            {
                // NEW student
                request.CreatedBy = user;
                request.UpdatedBy = null;
            }
            else
            {
                request.UpdatedBy = user;
            }

            return await _repository.AddOrUpdateAsync(request);
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResultModel> GetByFilterAsync(string? name, string? email )
        {
            return await _repository.GetByFilterAsync(name, email);
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }
    }
}
