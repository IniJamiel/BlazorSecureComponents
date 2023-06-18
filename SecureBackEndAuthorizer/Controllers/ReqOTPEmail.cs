using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonModelsLib;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class ReqOTPEmail : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> ReqOTPTask([FromBody] string email)
    {
        var user = await UserContext.GetUserBaseByEmail(email);
        if (user == null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return ("User Not Found");
        }

        var client = new SmtpClient(SMTPSettings.Server, SMTPSettings.Port)
        {
            Credentials = new NetworkCredential(SMTPSettings.SenderEmail, SMTPSettings.SenderPassword),
            EnableSsl = true
        };
        client.Send(SMTPSettings.SenderEmail, user.Email, SMTPSettings.Header, SMTPSettings.Body + StaticOTP.GenerateOTP(user.Username));
        return ("sent");
    }
}
