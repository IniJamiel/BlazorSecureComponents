using System.Net.Http.Json;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SecureBackEndAuthorizer.Controllers
{
    [Route("api/[Controller]")]
    public class Login : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [EnableCors]
        public async Task<UserBase> LoginTask(string id, string password)
        {
            var user = await UserContext.Obj.userBases.Where(a => (a.Email == id || a.Username == id) && password.Equals(a.Password)).FirstOrDefaultAsync();
            return user;
        }
    }
}
