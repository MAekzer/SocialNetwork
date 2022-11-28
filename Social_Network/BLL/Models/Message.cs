using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Sender_id { get; set; }
        public int Receiver_id { get; set; }
    }
}
