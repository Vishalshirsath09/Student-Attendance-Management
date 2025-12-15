using System.ComponentModel.DataAnnotations;

namespace Kemar.SMS.Model.Request
{
    public class StudentRequest
    {
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [Required]
        public int Rollno { get; set; }

        [Required]
        [MaxLength(50)]
        public string Class { get; set; }

        [Required]
        [MaxLength(5)]
        public string Div { get; set; }

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
