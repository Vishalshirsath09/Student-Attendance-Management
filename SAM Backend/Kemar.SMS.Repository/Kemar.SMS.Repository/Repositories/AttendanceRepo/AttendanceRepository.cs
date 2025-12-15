using AutoMapper;
using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Repository.Repositories.AttendanceRepo
{
    public class AttendanceRepository : IAttendance
    {
        private readonly KemarDBContext _context;
        private readonly IMapper _mapper;

        public AttendanceRepository(KemarDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> AddOrUpdateAsync(AttendanceRequest request)
        {
            try
            {
                if (request.AttendanceId == 0)
                {
                    var attendance = _mapper.Map<Attendance>(request);
                  //  attendance.TeacherId = request.TeacherId;
                    attendance.CreatedAt = DateTime.UtcNow;
                    attendance.IsActive = true;

                    _context.Attendances.Add(attendance);
                    await _context.SaveChangesAsync();

                    return ResultModel.Created(_mapper.Map<AttendanceResponse>(attendance));
                }

                var existing = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.AttendanceId == request.AttendanceId && a.IsActive);

                if (existing == null)
                    return ResultModel.NotFound("Attendance not found");

                existing.StudentId = request.StudentId;
                existing.SubjectId = request.SubjectId;
                existing.TeacherId = request.TeacherId;
                existing.IsPresent = request.IsPresent;
                existing.AttendanceDate = DateTime.UtcNow;
                existing.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return ResultModel.Updated(_mapper.Map<AttendanceResponse>(existing));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.AttendanceId == id && a.IsActive);

            if (attendance == null)
                return ResultModel.NotFound("Attendance not found");

            return ResultModel.Success(_mapper.Map<AttendanceResponse>(attendance));
        }

        public async Task<ResultModel> GetByFilterAsync(int? studentId, int? subjectId, int? teacherId, DateTime? date)
        {
            var query = _context.Attendances.Where(a => a.IsActive);

            if (studentId.HasValue)
                query = query.Where(a => a.StudentId == studentId.Value);

            if (subjectId.HasValue)
                query = query.Where(a => a.SubjectId == subjectId.Value);

            if (teacherId.HasValue)
                query = query.Where(a => a.TeacherId == teacherId.Value);

            if (date.HasValue)
                query = query.Where(a => a.AttendanceDate.Date == date.Value.Date);

            var list = await query.ToListAsync();

            return ResultModel.Success(_mapper.Map<List<AttendanceResponse>>(list));
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.AttendanceId == id && a.IsActive);

            if (attendance == null)
                return ResultModel.NotFound("Attendance not found");

            attendance.IsActive = false;
            attendance.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ResultModel.Success(null, "Attendance deleted successfully");
        }
    }
}
