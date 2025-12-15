using Kemar.SMS.Model.Response.Common;
using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class StudentRequest : CommonResponse
    {
        public int StudentId { get; set; } 

        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public int? Rollno { get; set; }

        [Required]
        [MaxLength(10)]
        public string Class { get; set; } = string.Empty;

        [Required]
        [MaxLength(5)]
        public string Div { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? PhoneNo { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [EmailAddress]
        [MaxLength(150)]
        public string? EmailAddress { get; set; }

    }
}
