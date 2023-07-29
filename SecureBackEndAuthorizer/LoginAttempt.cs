using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBackEndAuthorizer
{
    internal class LoginAttempt
    {
        internal static List<LoginAttempt> loginAttempts = new();
        internal static bool CheckLoginAttemptValid(string username)
        {
            loginAttempts.RemoveAll(a => a.attemptTime <= DateTime.Now.AddMinutes(-60));
            if (loginAttempts.Where(a => a.username == username).Count() >= 3)
            {
                loginAttempts.Add(new LoginAttempt() { username = username });
                return false;
            }
            loginAttempts.Add(new LoginAttempt() { username = username });
            return true;
        }
        internal static void LoginAttemptClear(string username)
        {
            loginAttempts.RemoveAll(a => a.username == username);
        }

        string username = "";
        DateTime attemptTime = DateTime.Now;



    }


}
