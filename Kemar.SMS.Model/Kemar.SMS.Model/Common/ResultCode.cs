namespace Kemar.SMS.Model.Common
{
    public enum ResultCode
    {
        Success = 200,
        SuccessfullyCreated = 201,
        SuccessfullyUpdated = 202,
        DuplicateRecord = 409,
        RecordNotFound = 404,
        Invalid = 400,
        Unauthorized = 401,
        NotAllowed = 405,
        ExceptionThrown = 500
    }
}
