using Kemar.SMS.Model.Response.Common;

namespace Kemar.SMS.Model.Response
{
    public class StudentResponse : CommonResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int Rollno { get; set; }
        public string Class { get; set; } = string.Empty;
        public string Div { get; set; } = string.Empty;
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
    }
}
