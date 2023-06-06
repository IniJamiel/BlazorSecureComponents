using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeComps.UtilLib
{
    public class StateManager
    {
        private string? sessionID;
        private DateTime timeCreate;
        private const int timeOut = 900;

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
