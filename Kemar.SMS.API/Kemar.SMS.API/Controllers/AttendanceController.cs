//using Kemar.SMS.Business.AttendanceBusiness;
//using Kemar.SMS.Model.Request;
//using Microsoft.AspNetCore.Mvc;

//namespace Kemar.SMS.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AttendanceController : ControllerBase
//    {
//        private readonly IAttendanceService _attendanceService;

//        public AttendanceController(IAttendanceService attendanceService)
//        {
//            _attendanceService = attendanceService;
//        }

//        [HttpPost("CreateAttendance")]
//        public async Task<IActionResult> Create(AttendanceRequest request)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var result = await _attendanceService.CreateAsync(request);
//            return Ok(result);
//        }

//        [HttpGet("GetAllAttendances")]
//        public async Task<IActionResult> GetAll()
//        {
//            var result = await _attendanceService.GetAllAsync();
//            return Ok(result);
//        }

//        [HttpGet("GetAttendanceById/{id:int}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var result = await _attendanceService.GetByIdAsync(id);
//            if (result == null)
//                return NotFound("attendance not found");

//            return Ok(result);
//        }

//        [HttpPut("UpdateAttendance/{id:int}")]
//        public async Task<IActionResult> Update(int id, AttendanceRequest request)
//        {
//            var result = await _attendanceService.UpdateAsync(id, request);
//            if (result == null)
//                return NotFound("attendance not found");

//            return Ok(result);
//        }

//        [HttpDelete("RemoveAttendance/{id:int}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var result = await _attendanceService.DeleteAsync(id);

//            if (!result)
//                return NotFound("attendance not found");

//            return Ok("attendance deleted successfully");
//        }
//    }
//}
