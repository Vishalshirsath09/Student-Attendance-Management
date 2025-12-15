using Kemar.SMS.Repository.Entity.BaseEntites;

namespace Kemar.SMS.Repository.Entity
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
    }
}
