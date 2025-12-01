using Kemar.SMS.Repository.Entity.BaseEntites;

namespace Kemar.SMS.Repository.Entity
{
    public class Student : BaseEntity
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Rollno { get; set; }
        public string Class { get; set; }
        public string Div { get; set; }
        public string PhoneNo { get; set; } 
        public string Address { get; set; }
        public string? EmailAddress { get; set; }
        public ICollection<Attendance>? Attendances { get; set; } = new List<Attendance>();
    }
}
