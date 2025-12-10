using Kemar.SMS.Model.Response.Common;
using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class AttendanceRequest : CommonResponse
    {
        public int AttendanceId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public bool IsPresent { get; set; }
        public DateTime? AttendanceDate { get; set; } = DateTime.UtcNow;
    }
}
