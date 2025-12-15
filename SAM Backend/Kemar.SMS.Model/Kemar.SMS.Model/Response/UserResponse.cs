using Kemar.SMS.Model.Response.Common;

namespace Kemar.SMS.Model.Response
{
    public class UserResponse : CommonResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public string? TeacherName { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailAddress { get; set; }
        public string? Qualification { get; set; }
        public decimal? Experience { get; set; }
        public string? Address { get; set; }
        public string? StudentName { get; set; }
        public string? Class { get; set; }
        public string? Div { get; set; }
        public int? Rollno { get; set; }
    }
}
