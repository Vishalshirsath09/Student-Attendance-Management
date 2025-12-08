//using Kemar.SMS.Business.SubjectBusiness;
//using Kemar.SMS.Model.Request;
//using Microsoft.AspNetCore.Mvc;

//namespace Kemar.SMS.API.Controllers
//{
//    [ApiController]
//    [Route("api/[Controller]")]
//    public class SubjectController : ControllerBase
//    {
//        private readonly ISubjectService  _subjectService;

//        public SubjectController(ISubjectService SubjectService)
//        {

//            _subjectService = SubjectService;
//        }
//        [HttpPost("CreateSubject")]
//        public async Task<IActionResult> Create(SubjectRequest request)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            var result = await _subjectService.CreateAsync(request);
//            return Ok(result);
//        }

//        [HttpGet("GetAllSubject")]
//        public async Task<IActionResult> GetAll()
//        {
//            var result = await _subjectService.GetAllAsync();
//            return Ok(result);
//        }

//        [HttpGet("GetSubjectById/{id:int}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var result = await _subjectService.GetByIdAsync(id);
//            if (result == null) return NotFound("subject not found");
//            return Ok(result);
//        }

//        [HttpPut("UpdateSubject/{id:int}")]
//        public async Task<IActionResult> Update(int id, SubjectRequest request)
//        {
//            var result = await _subjectService.UpdateAsync(id, request);
//            if (result == null) return NotFound("subject not found");
//            return Ok(result);
//        }

//        [HttpDelete("RemoveTeacher/{id:int}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var result = await _subjectService.DeleteAsync(id);
//            if (!result) return NotFound("subject not found");
//            return Ok("subject deleted successfully");
//        }
//    }
//}
