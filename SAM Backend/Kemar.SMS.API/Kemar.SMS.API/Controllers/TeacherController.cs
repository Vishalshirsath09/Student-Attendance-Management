using Kemar.SMS.Business.TeacherBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [Authorize(Roles = "HOD")]
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(TeacherRequest request)
        {
            var result = await _service.AddOrUpdateAsync(request);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }


        [Authorize(Roles = "HOD,Teacher")]
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _service.GetByFilterAsync(null, null);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD,Teacher")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD")]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string? name, string? email)
        {
            var result = await _service.GetByFilterAsync(name, email);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}
