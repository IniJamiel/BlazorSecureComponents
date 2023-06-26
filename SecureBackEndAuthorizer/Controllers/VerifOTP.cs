using System.Security.Cryptography.X509Certificates;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class VerifOTP : Controller
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<bool> VerifOTPTask([FromBody] ResetObj resetOBj)
    {
        var user = await UserContext.GetUserBaseByEmail(resetOBj.Id);

        var returnObj = await StaticOTP.VerifyOTPAsync(user.Username, resetOBj.OTP);
        return returnObj;
    }
}