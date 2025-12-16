using Kemar.SMS.Business.SubjectBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kemar.SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [Authorize(Roles = "HOD")]
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] SubjectRequest request)
        {
            // Get logged-in username from token
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
                throw new UnauthorizedAccessException("User not found in token");

            if (request.SubjectId == 0)
                request.CreatedBy = username;   // For new record
            else
                request.UpdatedBy = username;   // For update

            var result = await _service.AddOrUpdateAsync(request);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD,Teacher")]
        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
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

        [Authorize(Roles = "HOD,Teacher")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string? subjectName, [FromQuery] string? subjectCode)
        {
            var result = await _service.GetByFilterAsync(subjectName, subjectCode);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD")]
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
