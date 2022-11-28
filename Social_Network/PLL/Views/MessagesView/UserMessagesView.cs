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

namespace SocialNetwork.PLL.Views.MessagesView
{
    public class UserMessagesView : View
    {
        MessageService messageService;

        public UserMessagesView(UserService userService, MessageService messageService) : base(userService)
        {
            this.messageService = messageService;
        }

        public void Show(User user)
        {
            int incomingMessages = messageService.GetReceived(user).Count();

            Console.Clear();
            Console.WriteLine($"Входящих сообщений: {incomingMessages}\n");
            Console.WriteLine("Написать сообщение (Нажмите 1)");
            Console.WriteLine("К входящим сообщениям (нажмите 2)");
            Console.WriteLine("К отправленным сообщениям (нажмите 3)");
            Console.WriteLine("Назад в главное меню (нажмите 4)");
            Console.WriteLine();

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Укажите email получателя: ");
                    string email = Console.ReadLine();
                    Console.WriteLine("Текст сообщения (не более 5000 символов):");
                    string text = Console.ReadLine();

                    MessageData messageData = new()
                    {
                        Content = text,
                        Sender_id = user.Id,
                        Email = email
                    };

                    try
                    {
                        messageService.Send(messageData);
                        Success.Message("Сообщение успешно отправлено.");
                        Show(user);
                    }
                    catch (UserNotFoundException)
                    {
                        Alert.Message("Пользователя с таким почтовым адресом не существует.");
                        Show(user);
                    }
                    catch (ArgumentNullException)
                    {
                        Alert.Message("Неверный формат ввода (пустое сообщение или превышен лимит символов).");
                        Show(user);
                    }
                    catch (Exception)
                    {
                        Alert.Message("Произошла непредвиденная ошибка. Сообщение не удалось отправить.");
                        Show(user);
                    }
                    break;
                case "2":
                    Program.incomingMessagesView.Show(user);
                    break;
                case "3":
                    Program.outcomingMessagesView.Show(user);
                    break;
                case "4":
                    Program.userMenuView.Show(user);
                    break;
                default:
                    Show(user);
                    break;
            }
        }
    }
}
