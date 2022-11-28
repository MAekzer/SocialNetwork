using Social_Network.DAL.Entities;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class MessageService
    {
        IUserRepository userRepository;
        IMessageRepository messageRepository;

        public MessageService(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
        }

        public void Send(MessageData data)
        {
            if (String.IsNullOrEmpty(data.Content))
                throw new ArgumentNullException();

            if (data.Content.Length > 5000)
                throw new ArgumentNullException();

            var recipient = userRepository.FindByEmail(data.Email);
            if (recipient == null) throw new UserNotFoundException();

            MessageEntity messageEntity = new()
            {
                content = data.Content,
                receiver_id = recipient.id,
                sender_id = data.Sender_id
            };

            if (messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }

        public Message ConstructMessageModel(MessageEntity messageEntity)
        {
            return new Message()
            {
                Id = messageEntity.id,
                Content = messageEntity.content,
                Sender_id = messageEntity.sender_id,
                Receiver_id = messageEntity.receiver_id
            };
        }

        public IEnumerable<Message> GetReceived(User receiver)
        {
            var messageEntities = messageRepository.FindByRecipientId(receiver.Id);

            return from entity in messageEntities
                   select ConstructMessageModel(entity);
        }

        public IEnumerable<Message> GetSended(User sender)
        {
            var messageEntities = messageRepository.FindBySenderId(sender.Id);

            return from entity in messageEntities
                   select ConstructMessageModel(entity);
        }

        public IEnumerable<Message> GetReceived(User receiver, string senderEmail)
        {
            UserEntity sender = userRepository.FindByEmail(senderEmail);
            if (sender is null) throw new UserNotFoundException();

            var messageEntities = messageRepository.FindByRecipientId(receiver.Id);

            return from entity in messageEntities
                   where entity.sender_id == sender.id
                   select ConstructMessageModel(entity);
        }

        public IEnumerable<Message> GetSended(User sender, string receiverEmail)
        {
            UserEntity receiver = userRepository.FindByEmail(receiverEmail);
            if (receiver is null) throw new UserNotFoundException();

            var messageEntities = messageRepository.FindBySenderId(sender.Id);

            return from entity in messageEntities
                   where entity.receiver_id == receiver.id
                   select ConstructMessageModel(entity);
        }
    }
}
