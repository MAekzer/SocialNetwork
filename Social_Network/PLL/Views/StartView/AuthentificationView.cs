using Social_Network;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views.StartView
{
    public class AuthentificationView : View
    {
        public AuthentificationView(UserService userService) : base(userService) { }

        public void Show()
        {
            Console.Clear();
            var authenticationData = new UserAuthenticationData();

            Console.WriteLine("Введите почтовый адрес:");
            authenticationData.Email = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            authenticationData.Password = Console.ReadLine();

            try
            {
                var user = userService.Authenticate(authenticationData);
                Success.Message($"Вы успешно вошли в социальную сеть! Добро пожаловать {user.FirstName}");
                Program.userMenuView.Show(user);
            }
            catch (UserNotFoundException)
            {
                Alert.Message("Пользователь не найден.");
                Program.mainView.Show();
            }
            catch (WrongPasswordException)
            {
                Alert.Message("Неверный пароль.");
                Program.mainView.Show();
            }
        }
    }
}
