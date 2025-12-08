using Kemar.SMS.Business.TeacherBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(TeacherRequest request)
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
        public async Task<IActionResult> Filter(string? name, string? email)
        {
            var result = await _service.GetByFilterAsync(name, email);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}
