using Kemar.SMS.Business.AttendanceBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kemar.SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] AttendanceRequest request)
        {
            
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
                throw new UnauthorizedAccessException("User not found in token");

            if (request.AttendanceId == 0)
                request.CreatedBy = username;   
            else
                request.UpdatedBy = username; 

            var result = await _service.AddOrUpdateAsync(request);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD,Teacher")]
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _service.GetByFilterAsync(null, null, null,null);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD,Teacher,Student")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD,Teacher")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] int? studentId, [FromQuery] int? subjectId, [FromQuery] int? teacherId, [FromQuery] DateTime? date)
        {
            var result = await _service.GetByFilterAsync(studentId,subjectId,teacherId,date);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD,Teacher")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
                throw new UnauthorizedAccessException("User not found in token");

            var result = await _service.DeleteByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}
