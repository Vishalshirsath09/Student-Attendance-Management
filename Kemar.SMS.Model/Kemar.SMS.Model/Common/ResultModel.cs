namespace Kemar.SMS.Model.Common
{
    public class ResultModel
    {
        public ResultCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ResultModel Success(object data, string msg = "Success")
            => new() { StatusCode = ResultCode.Success, Message = msg, Data = data };

        public static ResultModel Created(object data)
            => new() { StatusCode = ResultCode.SuccessfullyCreated, Message = "Created Successfully",Data= data};

        public static ResultModel Updated(object data)
            => new() { StatusCode = ResultCode.SuccessfullyUpdated, Message = "Updated Successfully"};

        public static ResultModel NotFound(string msg)
            => new() { StatusCode = ResultCode.RecordNotFound, Message = "Record not found"};

        public static ResultModel Error(string msg)
            => new() { StatusCode = ResultCode.ExceptionThrown, Message = msg};
    }
}
