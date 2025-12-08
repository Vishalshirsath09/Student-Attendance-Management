using AutoMapper;
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

        public async Task<AttendanceResponse> CreateAsync(AttendanceRequest request)
        {
            var attendance = _mapper.Map<Attendance>(request);

            attendance.CreatedBy = "System";
            attendance.CreatedAt = DateTime.UtcNow;
            attendance.UpdatedBy = "System";
            attendance.UpdatedAt = DateTime.UtcNow;
            attendance.IsActive = true;

            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();

            return _mapper.Map<AttendanceResponse>(attendance);
        }

        public async Task<IEnumerable<AttendanceResponse>> GetAllAsync()
        {
            var attendances = await _context.Attendances
                .Where(x => x.IsActive)
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AttendanceResponse>>(attendances);
        }

        public async Task<AttendanceResponse?> GetByIdAsync(int id)
        {
            var attendance = await _context.Attendances
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.AttendanceId == id && x.IsActive);

            return attendance == null ? null : _mapper.Map<AttendanceResponse>(attendance);
        }

        public async Task<AttendanceResponse?> UpdateAsync(int id, AttendanceRequest request)
        {
            var existingAttendance = await _context.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id && x.IsActive);

            if (existingAttendance == null)
                return null;

            _mapper.Map(request, existingAttendance);

            existingAttendance.UpdatedBy = "System";
            existingAttendance.UpdatedAt = DateTime.UtcNow;

            _context.Attendances.Update(existingAttendance);
            await _context.SaveChangesAsync();

            return _mapper.Map<AttendanceResponse>(existingAttendance);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id && x.IsActive);

            if (attendance == null)
                return false;

            attendance.IsActive = false;
            attendance.UpdatedAt = DateTime.UtcNow;
            attendance.UpdatedBy = "System";

            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
