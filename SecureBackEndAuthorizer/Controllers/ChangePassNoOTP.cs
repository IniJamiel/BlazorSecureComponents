using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class ChangePassNoOTP : Controller
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<bool> changePassNoOTP([FromBody] CommonModelsLib.ChangePassword resetObj)
    {
        var context = UserContext.Obj;
        var toBeChanged = await context.userBases.Where(a => a.Email == resetObj.Email).FirstOrDefaultAsync();
        if (toBeChanged != null)
        {
            toBeChanged.Password = await Encryption.Hash(resetObj.Password);
            int saved = await context.SaveChangesAsync();
            if (saved != 0)
            {
                return true;
            }
        }
        return false;

    }
}