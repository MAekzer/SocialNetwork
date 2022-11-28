using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.PLL.Views;
using SocialNetwork.PLL.Views.MessagesView;
using SocialNetwork.PLL.Views.ProfileView;
using SocialNetwork.PLL.Views.StartView;

namespace Social_Network
{
    internal class Program
    {
        static IUserRepository userRepository = new UserRepository();
        static IMessageRepository messageRepository = new MessageRepository();
        static IFriendRepository friendRepository = new FriendRepository();
        static UserService userService = new(userRepository);
        static MessageService messageService = new(userRepository, messageRepository);
        static FriendService friendService = new(userRepository, friendRepository);
        public static MainView mainView = new(userService);
        public static RegistrationView registrationView = new(userService);
        public static AuthentificationView authentificationView = new(userService);
        public static UserMenuView userMenuView = new(userService, messageService);
        public static UserInfoView userInfoView = new(userService);
        public static UpdateProfileView updateProfileView = new(userService);
        public static UserFriendsView userFriendsView = new(userService, friendService);
        public static UserMessagesView userMessagesView = new(userService, messageService);
        public static IncomingMessagesView incomingMessagesView = new(userService, messageService);
        public static OutcomingMessagesView outcomingMessagesView = new(userService, messageService);

        static void Main()
        {
            mainView.Show();
        }
    }
}