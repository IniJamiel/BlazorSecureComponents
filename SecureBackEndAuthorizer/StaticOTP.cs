using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CommonModelsLib;
using Microsoft.AspNetCore.Routing.Constraints;
using OtpNet;


namespace SecureBackEndAuthorizer
{
    public static class StaticOTP
    {

        public static string GenerateOTP(string username)
        {
            byte[] bytes = Encoding.UTF32.GetBytes(username);
            return new Totp(bytes, step: MainSettings.OTPtimeout, totpSize: MainSettings.OTPlength).ComputeTotp();
        }

        public static bool VerifyOTP(string username, string OTP)
        {
            long timeStepMatched;
            byte[] bytes = Encoding.UTF32.GetBytes(username);
            return new Totp(bytes, step: MainSettings.OTPtimeout, totpSize: MainSettings.OTPlength).VerifyTotp(OTP, out timeStepMatched);
        }
        public async static Task<bool> VerifyOTPAsync(string username, string OTP)
        {
            long timeStepMatched;
            byte[] bytes = Encoding.UTF32.GetBytes(username);
            return new Totp(bytes, step: MainSettings.OTPtimeout, totpSize: MainSettings.OTPlength).VerifyTotp(OTP, out timeStepMatched);
        }
    }

}
