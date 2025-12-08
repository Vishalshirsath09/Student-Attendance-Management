using Kemar.SMS.Repository.Entity.BaseEntites;

namespace Kemar.SMS.Repository.Entity
{
    public class Teacher : BaseEntity
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
    }
}
