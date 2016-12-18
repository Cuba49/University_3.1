using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Clients
    {
        string Name;
        string Information;

        public Clients(string name, string information)
        {
            Name = name;
            Information = information;
        }

        public string ReturnName()
        {
            return Name;
        }
        public string ReturnInformation()
        {
            return Information;
        }

        public void ChoiseInformation(string inf)
        {
            Information = inf;
        }
    }
}
