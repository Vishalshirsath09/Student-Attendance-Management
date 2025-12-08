using Kemar.SMS.Model.Response.Common;
using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class UserRequest : CommonResponse
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }
        public string FullName { get; set; }
        public int? TeacherId { get; set; } 
        public string? TeacherName { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Class { get; set; }
        public string? Div { get; set; }
        public int Rollno { get; set; }

    }
}
