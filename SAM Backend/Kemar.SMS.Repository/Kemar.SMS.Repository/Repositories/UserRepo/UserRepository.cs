using AutoMapper;
using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Kemar.SMS.Model.Response;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kemar.SMS.Repository.Repositories.UserRepo
{
    public class UserRepository : IUser
    {
        private readonly KemarDBContext _context;
        private readonly IMapper _mapper;

        public UserRepository(KemarDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> AddOrUpdateAsync(UserRequest request)
        {
            try
            {
                if (request.UserId == 0)
                {
                    var user = _mapper.Map<User>(request);
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    user.CreatedAt = DateTime.UtcNow;
                    user.IsActive = true;

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    var response = _mapper.Map<UserResponse>(user);
                    return ResultModel.Created(response);
                }

                var existing = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == request.UserId && u.IsActive);

                if (existing == null)
                    return ResultModel.NotFound("User not found");

                existing.Username = request.Username;
                if (!string.IsNullOrWhiteSpace(request.Password))
                    existing.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                existing.Role = request.Role;
                existing.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var updatedResponse = _mapper.Map<UserResponse>(existing);
                return ResultModel.Updated(updatedResponse);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResultModel> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsActive);
            if (user == null)
                return ResultModel.NotFound("User not found");

            return ResultModel.Success(_mapper.Map<UserResponse>(user));
        }

        public async Task<ResultModel> GetByFilterAsync(string? username, string? role)
        {
            var query = _context.Users.Where(u => u.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(username))
                query = query.Where(u => u.Username.Contains(username));

            if (!string.IsNullOrEmpty(role))
                query = query.Where(u => u.Role == role);

            var users = await query.ToListAsync();
            if (!users.Any())
                return ResultModel.NotFound("No users found");

            return ResultModel.Success(_mapper.Map<List<UserResponse>>(users));
        }

        public async Task<ResultModel> DeleteByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsActive);
            if (user == null)
                return ResultModel.NotFound("User not found");

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ResultModel.Success(null, "User deleted successfully");
        }

        public async Task<ResultModel> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return ResultModel.Error("Invalid username or password");

            return ResultModel.Success(_mapper.Map<UserResponse>(user));
        }
    }
}
