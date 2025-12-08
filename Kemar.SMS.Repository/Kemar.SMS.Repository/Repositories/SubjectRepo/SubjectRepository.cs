using AutoMapper;
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

        public async Task<SubjectResponse> CreateAsync(SubjectRequest request)
        {
            var subject = _mapper.Map<Subject>(request);

            subject.CreatedBy = "System";
            subject.CreatedAt = DateTime.UtcNow;
            subject.UpdatedBy = "System";
            subject.UpdatedAt = DateTime.UtcNow;
            subject.IsActive = true;

            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubjectResponse>(subject);
        }

        public async Task<IEnumerable<SubjectResponse>> GetAllAsync()
        {
            var subjects = await _context.Subjects
                                         .Where(x => x.IsActive == true)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<SubjectResponse>>(subjects);
        }

        public async Task<SubjectResponse?> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects
                                        .FirstOrDefaultAsync(x =>
                                             x.SubjectId == id && x.IsActive == true);

            return subject == null ? null : _mapper.Map<SubjectResponse>(subject);
        }

        public async Task<SubjectResponse?> UpdateAsync(int id, SubjectRequest request)
        {
            var existingSubject = await _context.Subjects
                .FirstOrDefaultAsync(x =>
                 x.SubjectId == id && x.IsActive == true);

            if (existingSubject == null)
                return null;

            _mapper.Map(request, existingSubject);

            existingSubject.UpdatedBy = "System";
            existingSubject.UpdatedAt = DateTime.UtcNow;

            _context.Subjects.Update(existingSubject);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubjectResponse>(existingSubject);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await _context.Subjects
                                        .FirstOrDefaultAsync(x =>
                                             x.SubjectId == id && x.IsActive == true);

            if (subject == null)
                return false;

            subject.IsActive = false;
            subject.UpdatedAt = DateTime.UtcNow;
            subject.UpdatedBy = "System";

            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

