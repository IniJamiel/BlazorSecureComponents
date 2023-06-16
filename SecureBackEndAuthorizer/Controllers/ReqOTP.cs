using CommonModelsLib.Contexts;
using CommonModelsLib;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace SecureBackEndAuthorizer.Controllers;
[Route("api/[Controller]")]
public class ReqOTP : Controller
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> reqOTPTask(string userName)
    {
        var user = await UserContext.GetUserBaseByUserName(userName);
        if (user == null)
        {
            return ("User Not Found");
        }

        var client = new SmtpClient(SMTPSettings.Server, SMTPSettings.Port)
        {
            Credentials = new NetworkCredential(SMTPSettings.SenderEmail, SMTPSettings.SenderPassword),
            EnableSsl = true
        };
        client.Send(SMTPSettings.SenderEmail, user.Email, SMTPSettings.Header, SMTPSettings.Body + StaticOTP.GenerateOTP(userName));
        return ("oke");
    }
}