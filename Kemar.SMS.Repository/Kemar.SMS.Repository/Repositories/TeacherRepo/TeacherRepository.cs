using AutoMapper;
using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Repository.Repositories.TeacherRepo
{
    public class TeacherRespository : ITeacher
    {
        private readonly KemarDBContext _context;
        private readonly IMapper _mapper;

        public TeacherRespository(KemarDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> AddOrUpdateAsync(TeacherRequest request)
        {
            if (request.TeacherId == 0)
            {
                var teacher = _mapper.Map<Teacher>(request);

                teacher.CreatedBy = request.CreatedBy;
                teacher.CreatedAt = DateTime.UtcNow;
                teacher.IsActive = true;

                _context.Teachers.Add(teacher);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    return ResultModel.Error("Database error while creating student: " + ex.Message);
                }

                return ResultModel.Created(teacher);
            }

            var existing = await _context.Teachers
                           .FirstOrDefaultAsync(x => x.TeacherId == request.TeacherId && x.IsActive);

            if (existing == null)
                return ResultModel.NotFound("Teacher not found");

            existing.TeacherName = request.TeacherName;
            existing.Address = request.Address;
            existing.PhoneNo = request.PhoneNo;
            existing.EmailAddress = request.EmailAddress;
            existing.UpdatedBy = request.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return ResultModel.Error("Database error while updating student: " + ex.Message);
            }

            return ResultModel.Updated(existing);
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(x => x.TeacherId == id && x.IsActive);

            if (teacher == null)
                return ResultModel.NotFound("Teacher not found");

            return ResultModel.Success(teacher);
        }

        public async Task<ResultModel> GetByFilterAsync(string? name, string? email)
        {
            var query = _context.Teachers.Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.TeacherName.Contains(name));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.EmailAddress.Contains(email));

            return ResultModel.Success(await query.ToListAsync());
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                        .FirstOrDefaultAsync(x => x.TeacherId == id && x.IsActive);

            if (teacher == null)
                return ResultModel.NotFound("User not found");

            teacher.IsActive = false;
            teacher.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return ResultModel.Error("Database error while deleting student: " + ex.Message);
            }

            return ResultModel.Success(null, "User deleted successfully");
        }
    }
}
