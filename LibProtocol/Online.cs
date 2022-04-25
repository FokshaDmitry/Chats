using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProtocol
{
    [Serializable]
    public class Online
    {
        public List<string> OnlineUser { get; set; }
        public List<string> Message { get; set; }


    }
}
