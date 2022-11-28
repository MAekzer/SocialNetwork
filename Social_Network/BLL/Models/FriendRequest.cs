using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class FriendRequest
    {
        public int User_Id { get; set; }
        public string Friend_Email { get; set; }
    }
}
