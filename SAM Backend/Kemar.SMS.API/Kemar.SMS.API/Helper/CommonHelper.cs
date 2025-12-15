using Kemar.SMS.Model.Common;
using Kemar.SMS.Model.Request;
using Microsoft.AspNetCore.Mvc;

public static class CommonHelper
{
    public static void SetUserInformation(UserRequest request, HttpContext context, bool isNew)
    {
        var username = context.User.FindFirst("username")?.Value;

        if (string.IsNullOrEmpty(username))
            throw new UnauthorizedAccessException("Invalid User");

        if (isNew)
            request.CreatedBy = username;

        request.UpdatedBy = username;
    }


    public static IActionResult ReturnActionResultByStatus(ResultModel result, ControllerBase cntbase)
    {
        if (result == null)
            return cntbase.NotFound(result);

        else if (result.StatusCode == ResultCode.SuccessfullyCreated)
            return cntbase.Created("", result);

        else if (result.StatusCode == ResultCode.SuccessfullyUpdated)
            return cntbase.Ok(result);

        else if (result.StatusCode == ResultCode.Success)
            return cntbase.Ok(result);

        else if (result.StatusCode == ResultCode.Unauthorized)
            return cntbase.Unauthorized(result);

        else if (result.StatusCode == ResultCode.DuplicateRecord)
            return cntbase.Conflict(result);

        else if (result.StatusCode == ResultCode.RecordNotFound)
            return cntbase.NotFound(result);

        else if (result.StatusCode == ResultCode.NotAllowed)
            return cntbase.Forbid();

        else if (result.StatusCode == ResultCode.Invalid)
            return cntbase.BadRequest(result);

        else if (result.StatusCode == ResultCode.ExceptionThrown)
            return cntbase.StatusCode(500, result);

        return cntbase.StatusCode(500, result);
    }

}
