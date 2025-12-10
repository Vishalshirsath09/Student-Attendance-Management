using Kemar.SMS.Model.Response.Common;
using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class TeacherRequest : CommonResponse
    {

        public int TeacherId { get; set; }

        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TeacherName { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string? EmailAddress { get; set; }
        
        [MaxLength(100)]
        public string Qualification { get; set; }

        [MaxLength(20)]
        public decimal Experience { get; set; }
    }

}
