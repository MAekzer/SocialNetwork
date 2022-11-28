using Social_Network;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views.ProfileView
{
    public class UserInfoView : View
    {
        public UserInfoView(UserService userService) : base(userService) { }

        public void Show(User user)
        {
            Console.Clear();
            Console.WriteLine(
                $"Информация о моем профиле\n" +
                $"Мой идентификатор: {user.Id}\n" +
                $"Меня зовут: {user.FirstName}\n" +
                $"Моя фамилия: {user.LastName}\n" +
                $"Мой пароль: {user.Password}\n" +
                $"Мой почтовый адрес: {user.Email}\n" +
                $"Ссылка на моё фото: {user.Photo}\n" +
                $"Мой любимый фильм: {user.FavoriteMovie}\n" +
                $"Моя любимая книга: {user.FavoriteBook}\n\n");

            Console.WriteLine("Назад в главное меню (нажмите 1)");
            Console.WriteLine("Редактировать профиль (нажмите 2)");

            switch (Console.ReadLine())
            {
                case "1":
                    Program.userMenuView.Show(user);
                    break;
                case "2":
                    Program.updateProfileView.Show(user);
                    break;
                default:
                    Show(user);
                    break;
            }
        }
    }
}
