using Social_Network;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views.StartView
{
    public class RegistrationView : View
    {
        public RegistrationView(UserService userService) : base(userService) { }

        public void Show()
        {
            Console.Clear();
            var userRegistrationData = new UserRegistrationData();

            Console.WriteLine("Для создания нового профиля введите ваше имя:");
            userRegistrationData.FirstName = Console.ReadLine();

            Console.Write("Ваша фамилия:");
            userRegistrationData.LastName = Console.ReadLine();

            Console.Write("Пароль:");
            userRegistrationData.Password = Console.ReadLine();

            Console.Write("Почтовый адрес:");
            userRegistrationData.Email = Console.ReadLine();

            try
            {
                userService.Register(userRegistrationData);
                Success.Message("Регистрация прошла успешно! Теперь вы можете авторизоваться в главном меню.");
                Program.mainView.Show();
            }
            catch (ArgumentNullException)
            {
                Alert.Message("Введено некорректное значение для одного из регистрационных полей.\n" +
                    "Попробуйте еще раз, внимательно следуя инструкции.");
                Show();
            }
            catch (Exception)
            {
                Alert.Message("Произошла непредвиденная ошибка при регистрации.");
                Show();
            }
        }
    }
}
