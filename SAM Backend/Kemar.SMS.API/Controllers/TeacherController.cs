using Kemar.SMS.Business.TeacherBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.SMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase

    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService TeacherService)
        {
            _teacherService = TeacherService;
        }

        [HttpPost("CreateTeacher")]
        public async Task<IActionResult> Create(TeacherRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _teacherService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _teacherService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetStudentById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teacherService.GetByIdAsync(id);
            if (result == null) return NotFound("teacher not found");
            return Ok(result);
        }

        [HttpPut("UpdateTeacher/{id:int}")]
        public async Task<IActionResult> Update(int id, TeacherRequest request)
        {
            var result = await _teacherService.UpdateAsync(id, request);
            if (result == null) return NotFound("teacher not found");
            return Ok(result);
        }

        [HttpDelete("RemoveTeacher/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _teacherService.DeleteAsync(id);
            if (!result) return NotFound("teacher not found");
            return Ok("teacher deleted successfully");
        }
    }
}


