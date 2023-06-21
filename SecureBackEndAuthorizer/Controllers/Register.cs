using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class Register : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<long> RegisterTask([FromBody] UserBase obj)
    {
        UserContext uc = new UserContext(UserContext.options);
        obj.Password = await Encryption.Hash(obj.Password);
        await uc.userBases.AddAsync(obj);
        await uc.SaveChangesAsync();
        return obj.Id;

    }
}