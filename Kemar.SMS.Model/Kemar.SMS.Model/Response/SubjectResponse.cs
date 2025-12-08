using Kemar.SMS.Model.Response.Common;

namespace Kemar.SMS.Model.Response
{
    public class SubjectResponse : CommonResponse
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string SubjectName { get; set; }
         public string SubjectCode { get; set; }
         public string TeacherName { get; set; }
         
    }
 }
