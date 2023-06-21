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
        public async Task<UserBase?> LoginTask([FromBody] SentLoginObject slo)
        {
            var user = await UserContext.Obj.userBases.Where(a => (a.Email == slo.Id || a.Username == slo.Id)).FirstOrDefaultAsync();
            if (user != null)
            {
                var cek = await Encryption.Verify(slo.Password, Hashed: user.Password);
                if (cek)
                {
                    user.Password = "";
                }
                else
                {
                    user = null;
                }
            }
            return user;
        }
    }
}
