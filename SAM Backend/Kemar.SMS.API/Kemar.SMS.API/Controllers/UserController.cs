using Kemar.SMS.Business.UserBusiness;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kemar.SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "HOD")]
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] UserRequest request)
        {
            var loggedInUser = HttpContext.User.Identity?.Name
                               ?? HttpContext.User.FindFirst("username")?.Value
                               ?? HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            request.CreatedBy = loggedInUser;
            request.UpdatedBy = loggedInUser;

            var result = await _service.AddOrUpdateAsync(request);

            return CommonHelper.ReturnActionResultByStatus(result, this);
        }


        [Authorize(Roles = "HOD")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter(string? username, string? role)
        {
            var result = await _service.GetByFilterAsync(username, role);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "HOD")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.ValidateUserAsync(request.Username, request.Password);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}
