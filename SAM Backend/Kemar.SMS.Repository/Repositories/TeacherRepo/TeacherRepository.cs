using AutoMapper;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Repository.Repositories.TeacherRepo
{
    public class TeacherRepository : ITeacher
    {
        private readonly KemarDBContext _context;
        private readonly IMapper _mapper;

        public TeacherRepository(KemarDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherResponse> CreateAsync(TeacherRequest request)
        {
            var teacher = _mapper.Map<Teacher>(request);

            teacher.CreatedBy = "System";
            teacher.CreatedAt = DateTime.UtcNow;
            teacher.UpdatedBy = "System";
            teacher.UpdatedAt = DateTime.UtcNow;
            teacher.IsActive = true;

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherResponse>(teacher);
        }

        public async Task<IEnumerable<TeacherResponse>> GetAllAsync()
        {
            var teachers = await _context.Teachers
                                         .Where(x => x.IsActive == true)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<TeacherResponse>>(teachers);
        }

        public async Task<TeacherResponse?> GetByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                                        .FirstOrDefaultAsync(x =>
                                             x.TeacherId == id && x.IsActive == true);

            return teacher == null ? null : _mapper.Map<TeacherResponse>(teacher);
        }

        public async Task<TeacherResponse?> UpdateAsync(int id, TeacherRequest request)
        {
            var existingTeacher = await _context.Teachers
                .FirstOrDefaultAsync(x =>
                 x.TeacherId == id && x.IsActive == true);

            if (existingTeacher == null)
                return null;

            _mapper.Map(request, existingTeacher);

            existingTeacher.UpdatedBy = "SYSTEM";
            existingTeacher.UpdatedAt = DateTime.UtcNow;

            _context.Teachers.Update(existingTeacher);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherResponse>(existingTeacher);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _context.Teachers
                                        .FirstOrDefaultAsync(x =>
                                             x.TeacherId == id && x.IsActive == true);

            if (teacher == null)
                return false;

            teacher.IsActive = false;
            teacher.UpdatedAt = DateTime.UtcNow;
            teacher.UpdatedBy = "SYSTEM";

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
