using Kemar.SMS.Repository.Entity.BaseEntites;

namespace Kemar.SMS.Repository.Entity
{
    public class Teacher : BaseEntity
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string? EmailAddress { get; set; }
        public ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
    }
}
