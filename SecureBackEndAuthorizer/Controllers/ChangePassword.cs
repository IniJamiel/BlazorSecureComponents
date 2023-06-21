using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtpNet;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class ChangePassword : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<bool> VerifOTPTask([FromBody] CommonModelsLib.ChangePassword resetObj)
    {
        var user = await UserContext.GetUserBaseByEmail(resetObj.Email);
        if (user != null)
        {
            var verif = await StaticOTP.VerifyOTPAsync(user.Username, resetObj.OTP);
            if (verif)
            {
                var userContext = new UserContext(UserContext.options);
                var changed = await userContext.userBases.Where(a => a.Id == user.Id).FirstOrDefaultAsync();
                changed.Password = await Encryption.Hash(resetObj.Password);
                int count = await userContext.SaveChangesAsync();
                if (count >= 0)
                {
                    return true;
                }
            }
        }

        return false;
    }
}