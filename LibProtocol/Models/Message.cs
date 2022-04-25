using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProtocol.Models
{
    [Serializable]
    public class Message
    {
        public Guid Id { get; set; }
        public string? Messages { get; set; }
        public DateTime dateMess { get; set; }
        public Guid IdUser { get; set; }

    }
}
