using AutoMapper;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Repository.Repositories.StudentRepo
{
    public class StudentRepository : IStudent
    {
        private readonly KemarDBContext _context;
        private readonly IMapper _mapper;

        public StudentRepository(KemarDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentResponse> CreateAsync(StudentRequest request)
        {
            var student = _mapper.Map<Student>(request);

            student.CreatedBy = "System";
            student.CreatedAt = DateTime.UtcNow;
            student.UpdatedBy = "System";
            student.UpdatedAt = DateTime.UtcNow;
            student.IsActive = true;

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentResponse>(student);
        }

        public async Task<IEnumerable<StudentResponse>> GetAllAsync()
        {
            var students = await _context.Students
                                         .Where(x => x.IsActive == true)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<StudentResponse>>(students);
        }

        public async Task<StudentResponse?> GetByIdAsync(int id)
        {
            var student = await _context.Students
                                        .FirstOrDefaultAsync(x =>
                                             x.StudentId == id && x.IsActive == true);

            return student == null ? null : _mapper.Map<StudentResponse>(student);
        }

        public async Task<StudentResponse?> UpdateAsync(int id, StudentRequest request)
        {
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(x =>
                 x.StudentId == id && x.IsActive == true);

            if (existingStudent == null)
                return null;

            _mapper.Map(request, existingStudent);

            existingStudent.UpdatedBy = "SYSTEM";
            existingStudent.UpdatedAt = DateTime.UtcNow;

            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentResponse>(existingStudent);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students
                                        .FirstOrDefaultAsync(x =>
                                             x.StudentId == id && x.IsActive == true);

            if (student == null)
                return false;

            student.IsActive = false;
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedBy = "SYSTEM";

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
