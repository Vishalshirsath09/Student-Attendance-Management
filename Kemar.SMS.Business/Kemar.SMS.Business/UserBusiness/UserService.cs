using Kemar.SMS.Business.StudentBusiness;
using Kemar.SMS.Business.TeacherBusiness;
using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Repositories.UserRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kemar.SMS.Business.UserBusiness
{
    public class UserService : IUserService
    {
        private readonly IUser _repository;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IConfiguration _config;

        public UserService(
            IUser repository,
            IStudentService studentService,
            ITeacherService teacherService,
            IConfiguration config)
        {
            _repository = repository;
            _studentService = studentService;
            _teacherService = teacherService;
            _config = config;
        }

        public async Task<ResultModel> AddOrUpdateAsync(UserRequest request)
        {
            var userResult = await _repository.AddOrUpdateAsync(request);

            if (userResult.StatusCode != ResultCode.SuccessfullyCreated)
                return userResult;

            var createdUser = userResult.Data as UserResponse;
            if (createdUser == null)
                return ResultModel.Error("User creation failed");

            if (request.Role == "Student")
            {
                var studentRequest = new StudentRequest
                {
                    UserId = createdUser.UserId,
                    StudentName = request.StudentName,
                    Rollno = request.Rollno,
                    Class = request.Class,
                    Div = request.Div,
                    PhoneNo = request.PhoneNo,
                    Address = request.Address,
                    EmailAddress = request.EmailAddress,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    IsActive = true
                };

                await _studentService.AddOrUpdateAsync(studentRequest);
            }

            else if (request.Role == "Teacher")
            {
                var teacherRequest = new TeacherRequest
                {
                    UserId = createdUser.UserId,
                    TeacherName = request.TeacherName,
                    PhoneNo = request.PhoneNo,
                    EmailAddress = request.EmailAddress,
                    Address = request.Address,
                    Qualification = request.Qualification,
                    Experience = (decimal)request.Experience,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    IsActive = true
                };

                await _teacherService.AddOrUpdateAsync(teacherRequest);
            }

            return userResult;
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResultModel> GetByFilterAsync(string? username, string? role)
        {
            return await _repository.GetByFilterAsync(username, role);
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<ResultModel> ValidateUserAsync(string username, string password)
        {
            var result = await _repository.ValidateUserAsync(username, password);
            if (result.StatusCode != ResultCode.Success)
                return result;

            var user = (UserResponse)result.Data;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim(ClaimTypes.Name, user.Username),
              new Claim("role", user.Role),
              new Claim("userId", user.UserId.ToString())
            };


            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:ExpiryInMinutes"])),
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return ResultModel.Success(new { user, token = tokenString });
        }
    }
}
