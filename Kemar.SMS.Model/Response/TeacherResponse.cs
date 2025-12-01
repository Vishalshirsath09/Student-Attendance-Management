using Kemar.SMS.Model.Response.Common;

namespace Kemar.SMS.Model.Response
{
    public  class TeacherResponse : CommonResponse
    {
        public int TecaherId { get; set; }
        public string TeacherName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string? EmailAddress { get; set; }
    }
}
