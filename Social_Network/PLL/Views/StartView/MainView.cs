using Social_Network;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views.StartView
{
    public class MainView : View
    {
        public MainView(UserService userService) : base(userService) { }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в социальную сеть.");
            Console.WriteLine();
            Console.WriteLine("Войти в профиль (нажмите 1)");
            Console.WriteLine("Зарегистрироваться (нажмите 2)");

            switch (Console.ReadLine())
            {
                case "1":
                    Program.authentificationView.Show();
                    break;
                case "2":
                    Program.registrationView.Show();
                    break;
                default:
                    Show();
                    break;
            }
        }
    }
}
