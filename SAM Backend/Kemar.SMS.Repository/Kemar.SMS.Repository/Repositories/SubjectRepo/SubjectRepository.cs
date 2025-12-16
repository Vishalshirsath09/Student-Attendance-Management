using AutoMapper;
using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Repository.Repositories.SubjectRepo
{
    public class SubjectRepository : ISubject
    {
        private readonly KemarDBContext _context;
        private readonly IMapper _mapper;

        public SubjectRepository(KemarDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> AddOrUpdateAsync(SubjectRequest request)
        {
            if (request.SubjectId == 0)
            {
                var subject = _mapper.Map<Subject>(request);
                subject.CreatedAt = DateTime.UtcNow;
                subject.IsActive = true;
                subject.CreatedBy = request.CreatedBy;

                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();

                // ✅ Reload with Teacher
                var created = await _context.Subjects
                    .Include(s => s.Teacher)
                    .FirstAsync(s => s.SubjectId == subject.SubjectId);

                return ResultModel.Created(_mapper.Map<SubjectResponse>(created));
            }

            var existing = await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectId == request.SubjectId && s.IsActive);

            if (existing == null)
                return ResultModel.NotFound("Subject not found");

            existing.SubjectName = request.SubjectName;
            existing.SubjectCode = request.SubjectCode;
            existing.TeacherId = request.TeacherId;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = request.UpdatedBy;

            await _context.SaveChangesAsync();

            // ✅ Reload with Teacher
            var updated = await _context.Subjects
                .Include(s => s.Teacher)
                .FirstAsync(s => s.SubjectId == existing.SubjectId);

            return ResultModel.Updated(_mapper.Map<SubjectResponse>(updated));
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(s => s.SubjectId == id && s.IsActive);

            if (subject == null)
                return ResultModel.NotFound("Subject not found");

            return ResultModel.Success(_mapper.Map<SubjectResponse>(subject));
        }

        public async Task<ResultModel> GetByFilterAsync(string? subjectName, string? subjectCode)
        {
            var query = _context.Subjects
                .Include(s => s.Teacher)
                .Where(s => s.IsActive);

            if (!string.IsNullOrWhiteSpace(subjectName))
                query = query.Where(s => s.SubjectName.Contains(subjectName));

            if (!string.IsNullOrWhiteSpace(subjectCode))
                query = query.Where(s => s.SubjectCode == subjectCode);

            var subjects = await query.ToListAsync();
            return ResultModel.Success(_mapper.Map<List<SubjectResponse>>(subjects));
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            var subject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectId == id && s.IsActive);

            if (subject == null)
                return ResultModel.NotFound("Subject not found");

            subject.IsActive = false;
            subject.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ResultModel.Success(null, "Subject deleted successfully");
        }
    }
}
