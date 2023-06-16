using System.Security.Cryptography.X509Certificates;
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
    public async Task<bool> VerifOTPTask(string userName, string OTP)
    {
        return StaticOTP.VerifyOTP(userName, OTP);
    }
}