using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace SecureBackEndAuthorizer.Controllers
{
    [Route("api/[Controller]")]
    public class verifSSO : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserBaseWithToken> VerifOTPTask([FromBody] ResetObj resetOBj)
        {
            var verif = await StaticOTP.VerifyOTPAsync(resetOBj.Id, resetOBj.OTP);
            UserBaseWithToken user = null;
            if (verif)
            {
                user = (UserBaseWithToken) await UserContext.GetUserBaseByUserName(resetOBj.Id);

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

                user.Token = jwtToken;
            }
            return user;
        }

    }
}
