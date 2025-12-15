using Kemar.SMS.Repository.Entity.BaseEntites;

namespace Kemar.SMS.Repository.Entity
{
    public class Subject : BaseEntity
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string TeacherName { get; set; }
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}

