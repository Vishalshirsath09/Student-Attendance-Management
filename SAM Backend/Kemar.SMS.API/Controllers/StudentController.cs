using Kemar.SMS.Business.StudentBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.SMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _studentService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetStudentById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentService.GetByIdAsync(id);
            if (result == null) return NotFound("Student not found");
            return Ok(result);
        }

        [HttpPut("UpdateStudent/{id:int}")]
        public async Task<IActionResult> Update(int id, StudentRequest request)
        {
            var result = await _studentService.UpdateAsync(id, request);
            if (result == null) return NotFound("Student not found");
            return Ok(result);
        }

        [HttpDelete("RemoveStudent/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentService.DeleteAsync(id);
            if (!result) return NotFound("Student not found");
            return Ok("Student deleted successfully");
        }
    }
}


























//[HttpGet("by-name/{name}")]
//public async Task<IActionResult> GetByName(string name)
//{
//    var student = await _studentRepo.GetByNameAsysnc(name);
//    if (student == null)
//        return NotFound("Student not found");

//    return Ok(student);
//}