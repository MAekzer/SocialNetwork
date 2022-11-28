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
    public class UpdateProfileView : View
    {
        public UpdateProfileView(UserService userService) : base(userService) { }

        public void Show(User user)
        {
            Console.Clear();

            Console.Write("Меня зовут:");
            user.FirstName = Console.ReadLine();

            Console.Write("Моя фамилия:");
            user.LastName = Console.ReadLine();

            Console.Write("Ссылка на моё фото:");
            user.Photo = Console.ReadLine();

            Console.Write("Мой любимый фильм:");
            user.FavoriteMovie = Console.ReadLine();

            Console.Write("Моя любимая книга:");
            user.FavoriteBook = Console.ReadLine();

            userService.Update(user);

            Success.Message("Ваш профиль успешно обновлен.");
            Program.userInfoView.Show(user);
        }
    }
}
