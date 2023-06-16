using System.Reflection.Metadata.Ecma335;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class Register : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<long> RegisterTask(UserBase obj)
    {
        UserBase ub = (UserBase)obj;
        UserContext uc = new UserContext(UserContext.options);
        if (!obj.Validate().Item1)
            return 0;
        await uc.userBases.AddAsync((UserBase)obj);
        await uc.SaveChangesAsync();
        return ub.Id;
    }
}