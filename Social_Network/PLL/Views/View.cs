using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public abstract class View
    {
        private protected UserService userService;

        public View(UserService userService)
        {
            this.userService = userService;
        }
    }
}
