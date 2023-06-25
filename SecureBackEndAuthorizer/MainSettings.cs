using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBackEndAuthorizer
{
    public static class MainSettings
    {
        public static string Database = "ConnectionStrings:Database";
        public static string SMTPServer = "ConnectionStrings:SMTP:Host";
        public static string SMTPPort = "ConnectionStrings:SMTP:Port";
        public static string SMTPSender = "ConnectionStrings:SMTP:SenderEmail";
        public static string SMTPPassword = "ConnectionStrings:SMTP:SenderPassword";

        public static string JWTSecretKey = "RONALD00OSIUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU";
        public static string JWTAudience = "Penyidang";
        public static string JWTIssuer = "Jamiel";
    }
}
