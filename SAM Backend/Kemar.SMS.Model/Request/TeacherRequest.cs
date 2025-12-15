using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class TeacherRequest
    {
 
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TeacherName { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string? EmailAddress { get; set; }
    
}

}
