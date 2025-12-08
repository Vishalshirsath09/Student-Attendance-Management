using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public  class AttendanceRequest
    {
        public int AttendanceId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public DateTime? AttendanceDate { get; set; }

        [Required]
        public bool IsPresent { get; set; }

    }
}
