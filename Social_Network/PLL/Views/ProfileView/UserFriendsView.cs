using Social_Network;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views
{
    public class UserFriendsView : View
    {
        FriendService friendService;

        public UserFriendsView(
            UserService userService,
            FriendService friendService)
            : base(userService)
        {
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            Console.Clear();
            Console.WriteLine("Посмотреть моих друзей (нажмите 1)");
            Console.WriteLine("Добавить в друзья (нажмите 2)");
            Console.WriteLine("Удалить из друзей (нажмите 3)");
            Console.WriteLine("Посмотреть, кто добавил меня в друзья (нажмите 4)");
            Console.WriteLine("В главное меню (нажмите 5)");
            Console.WriteLine();

            switch (Console.ReadLine())
            {
                case "1":
                    var myFriends = friendService.GetAllFriends(user);
                    foreach (var friend in myFriends)
                    {
                        var userFriend = userService.FindById(friend.friend_id);
                        Console.WriteLine(userFriend.Email);
                        Console.WriteLine($"{userFriend.FirstName} {userFriend.LastName}\n");
                    }
                    Console.WriteLine("Назад (Enter)");
                    Console.ReadLine();
                    Show(user);
                    break;

                case "2":
                    Console.Write("Укажите почтовый адрес друга: ");
                    string email = Console.ReadLine();

                    FriendRequest request = new()
                    {
                        User_Id = user.Id,
                        Friend_Email = email
                    };

                    try
                    {
                        friendService.AddFriend(request);
                        Success.Message("Пользователь успешно добавлен в друзья.");
                        Show(user);
                    }
                    catch(UserNotFoundException)
                    {
                        Alert.Message("Пользователь не найден.");
                        Show(user);
                    }
                    catch(FriendAlreadyAddedException)
                    {
                        Alert.Message("Пользователь уже у вас в друзьях");
                        Show(user);
                    }
                    catch(Exception)
                    {
                        Alert.Message("Произошла непредвиденная ошибка.");
                        Show(user);
                    }
                    break;

                case "3":
                    Console.Write("Укажите почтовый адрес друга: ");
                    string _email = Console.ReadLine();

                    FriendRequest _request = new()
                    {
                        User_Id = user.Id,
                        Friend_Email = _email
                    };

                    try
                    {
                        friendService.DeleteFriend(_request);
                        Success.Message("Пользователь успешно удален из друзей.");
                        Show(user);
                    }
                    catch (UserNotFoundException)
                    {
                        Alert.Message("Пользователь не найден.");
                        Show(user);
                    }
                    catch (Exception)
                    {
                        Alert.Message("Произошла непредвиденная ошибка.");
                        Show(user);
                    }
                    break;

                case "4":
                    var wouldbefriends = friendService.GetWouldBeFriends(user);

                    foreach (var friend in wouldbefriends)
                    {
                        var _friend = userService.FindById(friend.user_id);
                        Console.WriteLine(_friend.Email);
                        Console.WriteLine($"{_friend.FirstName} {_friend.LastName}\n");
                    }
                    Console.WriteLine("Назад (Enter)");
                    Console.ReadLine();
                    Show(user);
                    break;

                case "5":
                    Program.userMenuView.Show(user);
                    break;

                default:
                    Show(user);
                    break;
            }
        }
    }
}
