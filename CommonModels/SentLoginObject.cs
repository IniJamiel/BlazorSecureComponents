using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModelsLib
{
    public class SentLoginObject
    {
        public SentLoginObject()
        {

        }
        private string type { set; get; }
        public string Id { set; get; } = "";
        public string Password { set; get; } = "";
    }
}
