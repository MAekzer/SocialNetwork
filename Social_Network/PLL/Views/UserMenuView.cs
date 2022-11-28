using Social_Network;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserMenuView : View
    {
        MessageService messageService;

        public UserMenuView(UserService userService, MessageService messageService) : base(userService)
        {
            this.messageService = messageService;
        }

        public void Show(User user)
        {
            Console.Clear();

            int incomingMessages = messageService.GetReceived(user).Count();
            Console.WriteLine($"Входящих сообщений: {incomingMessages}\n");

            Console.WriteLine("Мой профиль (нажмите 1)");
            Console.WriteLine("Редактировать профиль (нажмите 2)");
            Console.WriteLine("Друзья (нажмите 3)");
            Console.WriteLine("Сообщения (нажмите 4)");
            Console.WriteLine("Выйти (нажмите 5)");

            switch (Console.ReadLine())
            {
                case "1":
                    Program.userInfoView.Show(user);
                    break;
                case "2":
                    Program.updateProfileView.Show(user);
                    break;
                case "3":
                    Program.userFriendsView.Show(user);
                    break;
                case "4":
                    Program.userMessagesView.Show(user);
                    break;
                case "5":
                    Program.mainView.Show();
                    break;
                default:
                    Show(user);
                    break;
            }
        }
    }
}
