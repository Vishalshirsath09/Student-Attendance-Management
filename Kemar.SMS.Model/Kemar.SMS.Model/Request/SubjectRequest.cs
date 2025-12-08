using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class SubjectRequest
    {
        public int SubjectId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        [Required]
        [MaxLength(10)]
        public string SubjectCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string TeacherName { get; set; }
    }
}
