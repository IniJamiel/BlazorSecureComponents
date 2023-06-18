using CommonModelsLib.Contexts;
using CommonModelsLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecureBackEndAuthorizer.Controllers;

public class VerifOTPuName : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<bool> VerifOTPTask([FromBody] ResetObj resetOBj)
    {
        var returnObj = await StaticOTP.VerifyOTPAsync(resetOBj.email, resetOBj.OTP);
        return returnObj;
    }
}