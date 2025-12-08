using Kemar.SMS.Model.Response.Common;

namespace Kemar.SMS.Model.Response
{
    public class LoginResponse : CommonResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string? Token { get; set; }
    }
}
