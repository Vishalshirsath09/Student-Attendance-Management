using Kemar.SMS.Model.Response.Common;
using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class SubjectRequest : CommonResponse
    {
        public int SubjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        [Required]
        [MaxLength(10)]
        public string SubjectCode { get; set; }

        [Required]
        public int TeacherId { get; set; }
    }
}
