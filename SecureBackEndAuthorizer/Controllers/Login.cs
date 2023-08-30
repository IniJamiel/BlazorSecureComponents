using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace SecureBackEndAuthorizer.Controllers
{
    [Route("api/[Controller]")]
    public class Login : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [EnableCors]
        public async Task<UserBaseWithToken?> LoginTask([FromBody] SentLoginObject slo)
        {
            var user = await UserContext.Obj.userBases.Where(a => (a.Email == slo.Id || a.Username == slo.Id))
                .FirstOrDefaultAsync();
            if (!LoginAttempt.CheckLoginAttemptValid(slo.Id))
            {
                var userContext = new UserContext(UserContext.options);
                var changed = await userContext.userBases.Where(a => a.Id == user.Id).FirstOrDefaultAsync();
                changed.Locked = true;
                int count = await userContext.SaveChangesAsync();
            }
            if (user != null)
            {
                var cek = await Encryption.Verify(slo.Password, Hashed: user.Password);
                if (user.Locked)
                {
                    user.Id = 0;
                    user.Username = "locked";
                    user.Email = "locked";
                    user.Locked = true;
                    UserBaseWithToken userWithTokenLocked = new UserBaseWithToken
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = "", // Password is already set to empty string in user object
                        Username = user.Username,
                        Birthday = user.Birthday,
                        PhoneNumber = user.PhoneNumber,
                        Locked = user.Locked,
                        Token = "Locked"
                    };
                    return userWithTokenLocked;
                }
                if (cek)
                {
                    // Create and add JWT token here as described before
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(MainSettings.JWTSecretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Username.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature),
                        Audience = MainSettings.JWTAudience,
                        Issuer = MainSettings.JWTIssuer,
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    user.Password = "";
                    UserBaseWithToken userWithToken = new UserBaseWithToken
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = "", // Password is already set to empty string in user object
                        Username = user.Username,
                        Birthday = user.Birthday,
                        PhoneNumber = user.PhoneNumber,
                        Token = jwtToken
                    };
                    LoginAttempt.LoginAttemptClear(slo.Id);
                    return userWithToken;
                }
                else
                {
                    user = null;
                }
            }

            return null;
        }
    }
}
