using AutoMapper;
using Kemar.SMS.Model.Common;
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

        public async Task<ResultModel> AddOrUpdateAsync(StudentRequest request)
        {
            try
            {
                if (request.StudentId == 0)
                {
                    // CREATE NEW STUDENT
                    var student = _mapper.Map<Student>(request);
                    student.CreatedAt = DateTime.UtcNow;
                    student.IsActive = true;

                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();

                    return ResultModel.Created(_mapper.Map<StudentResponse>(student));
                }

                // UPDATE EXISTING STUDENT
                var existing = await _context.Students
                    .FirstOrDefaultAsync(s => s.StudentId == request.StudentId && s.IsActive);

                if (existing == null)
                    return ResultModel.NotFound("Student not found");

                // ❌ Do NOT update UserId during update
                // existing.UserId = request.UserId;

                // Update other fields
                existing.StudentName = request.StudentName;
                existing.Rollno = request.Rollno;
                existing.Class = request.Class;
                existing.Div = request.Div;
                existing.PhoneNo = request.PhoneNo;
                existing.Address = request.Address;
                existing.EmailAddress = request.EmailAddress;
                existing.IsActive = request.IsActive; // optional: update status
                existing.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return ResultModel.Updated(_mapper.Map<StudentResponse>(existing));
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                throw;
            }
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id && s.IsActive);

            if (student == null)
                return ResultModel.NotFound("Student not found");

            return ResultModel.Success(_mapper.Map<StudentResponse>(student));
        }

        public async Task<ResultModel> GetByFilterAsync(string? name, string? className, string? div)
        {
            var query = _context.Students.Where(s => s.IsActive);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(s => s.StudentName.Contains(name));

            if (!string.IsNullOrWhiteSpace(className))
                query = query.Where(s => s.Class == className);

            if (!string.IsNullOrWhiteSpace(div))
                query = query.Where(s => s.Div == div);

            var students = await query.ToListAsync();
            return ResultModel.Success(_mapper.Map<List<StudentResponse>>(students));
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id && s.IsActive);

            if (student == null)
                return ResultModel.NotFound("Student not found");

            student.IsActive = false;
            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ResultModel.Success(null, "Student deleted successfully");
        }
    }
}
