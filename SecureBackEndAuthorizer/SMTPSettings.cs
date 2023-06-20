using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBackEndAuthorizer
{
    public static class SMTPSettings
    {
        public static string Server { get; set; }
        public static string SenderEmail { get; set; }
        public static string SenderPassword { get; set; }
        public static Int32 Port { get; set; }
        public static string Header { get; set; } = "OTP CODE";
        public static string Body { get; set; } = "Here is your OTP Code : ";
        
    }
}
