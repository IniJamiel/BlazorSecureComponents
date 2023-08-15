using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecureBackEndAuthorizer.Controllers
{
    [Route("api/[Controller]")]
    public class ReqSSO: ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [EnableCors]
        public async Task<Boolean?> ReqSSOTask(string userName, string baseURL)
        {
            var returned = new UserBaseWithToken();

            UserBase? uBase = await UserContext.GetUserBaseByUserName(userName);
            if (uBase != null)
            {
                string OTP = StaticOTP.GenerateOTPSSO(userName);

                var client = new SmtpClient(SMTPSettings.Server, SMTPSettings.Port)
                {
                    Credentials = new NetworkCredential(SMTPSettings.SenderEmail, SMTPSettings.SenderPassword),
                    EnableSsl = true
                };

                client.Send(SMTPSettings.SenderEmail, uBase.Email, SMTPSettings.Header, SMTPSettings.Body +baseURL+"/"+userName+"/"+OTP);

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
