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
    public class OutcomingMessagesView : View
    {
        MessageService messageService;

        public OutcomingMessagesView(UserService userService, MessageService messageService) : base(userService)
        {
            this.messageService = messageService;
        }

        public void Show(User user)
        {
            Console.Clear();
            Console.WriteLine("Посмотреть все отправленные сообщения (нажмите 1)");
            Console.WriteLine("Посмотреть сообщения, отправленные конкретному пользователю (нажмите 2)");
            Console.WriteLine("К входящим сообщениям (нажмите 3)");
            Console.WriteLine("Назад к сообщениям (нажмите 4)");
            Console.WriteLine("В главное меню (нажмите 5)");
            Console.WriteLine();

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    var incomingmessages = messageService.GetSended(user);

                    foreach (var message in incomingmessages)
                    {
                        Console.WriteLine($"\nПользователю {userService.FindById(message.Receiver_id).Email}: ");
                        Console.WriteLine(message.Content);
                    }

                    Console.WriteLine("\nНазад (Enter)");
                    Console.ReadLine();
                    Show(user);
                    break;

                case "2":
                    Console.Write("\nУкажите почтовый адрес пользователя: ");
                    string mail = Console.ReadLine();
                    Console.Clear();

                    try
                    {
                        var messages = messageService.GetSended(user, mail);
                        foreach (var message in messages)
                        {
                            Console.WriteLine($"Пользователю {userService.FindById(message.Receiver_id).Email}: ");
                            Console.WriteLine(message.Content);
                            Console.WriteLine();
                        }
                    }
                    catch (UserNotFoundException)
                    {
                        Alert.Message("Пользователь с таким почтовым адресом не найден.");
                        Show(user);
                    }
                    Console.WriteLine("\nНазад (Enter)");
                    Console.ReadLine();
                    Show(user);
                    break;

                case "3":
                    Program.outcomingMessagesView.Show(user);
                    break;

                case "4":
                    Program.userMessagesView.Show(user);
                    break;

                case "5":
                    Program.userMenuView.Show(user);
                    break;
            }
        }
    }
}
