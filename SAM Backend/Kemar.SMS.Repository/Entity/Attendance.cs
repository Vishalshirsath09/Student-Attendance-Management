using Kemar.SMS.Repository.Entity.BaseEntites;

namespace Kemar.SMS.Repository.Entity
{
    public class Attendance : BaseEntity
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime AttendanceDate { get; set; } = DateTime.UtcNow;  
        public bool IsPresent { get; set; }

        public Student? Student { get; set; } 
        public Subject? Subject { get; set; }
    }
}
