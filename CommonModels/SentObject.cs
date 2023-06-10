using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModelsLib
{
    public class SentObject
    {
        public SentObject(string type)
        {
            this.type = type;
        }
        private string type;
        public string Id = "";
        public string Password = "";

    }
}
