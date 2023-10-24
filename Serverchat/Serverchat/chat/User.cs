using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sockx
{
    [Serializable]
    internal class User
    {
        public string name;
        public string ipuser;
        public int port;

        public User(string name, string ipuser, int port)
        {
            this.name = name;
            this.ipuser = ipuser;
            this.port = port;

        }
    }
}
