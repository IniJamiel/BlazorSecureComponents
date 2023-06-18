using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonModelsLib;

namespace SeComps.UtilLib
{
    public class StateManager
    {
        private string? sessionID;
        private DateTime timeCreate;
        private const int timeOut = 900;

        private static string activeUserName;
        private static UserBase userBase;

        internal static void setActiveUserName(string UserName)
        {
            activeUserName = UserName;
        }
        internal static void setActiveUserBase(UserBase _userBase)
        {
            userBase = _userBase;
        }


        public static string GetActiveUsername()
        {
            return activeUserName;
        }

        public static UserBase GetUserBase()
        {
            return userBase;
        }

        public string? UserID
        {
            get => timeCreate.AddSeconds(timeOut) < DateTime.Now ? sessionID : null;
            set
            {
                sessionID = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
