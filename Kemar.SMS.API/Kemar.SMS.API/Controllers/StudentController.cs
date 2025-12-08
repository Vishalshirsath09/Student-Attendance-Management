using Kemar.SMS.Business.StudentBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] StudentRequest request)
        {
            var result = await _service.AddOrUpdateAsync(request);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string? name, [FromQuery] string? className, [FromQuery] string? div)
        {
            var result = await _service.GetByFilterAsync(name, className, div);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}
