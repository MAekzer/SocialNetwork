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
    public class FriendService
    {
        IUserRepository userRepository;
        IFriendRepository friendRepository;

        public FriendService(IUserRepository userRepository, IFriendRepository friendRepository)
        {
            this.userRepository = userRepository;
            this.friendRepository = friendRepository;
        }

        public void AddFriend(FriendRequest request)
        {
            var friend = userRepository.FindByEmail(request.Friend_Email);

            if (friend == null) throw new UserNotFoundException();
            if (IsFriend(request, out _)) throw new FriendAlreadyAddedException();

            FriendEntity friendEntity = new()
            {
                user_id = request.User_Id,
                friend_id = friend.id
            };

            if (friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }

        public void DeleteFriend(FriendRequest request)
        {
            if (!IsFriend(request, out FriendEntity friend)) throw new UserNotFoundException();

            if (friendRepository.Delete(friend.id) == 0) throw new Exception();
        }

        public bool IsFriend(FriendRequest request, out FriendEntity friend)
        {
            var friends = friendRepository.FindAllByUserId(request.User_Id);
            var userFriend = userRepository.FindByEmail(request.Friend_Email);

            if (userFriend == null) throw new UserNotFoundException();

            friend = friends.FirstOrDefault(user => user.friend_id == userFriend.id);

            if (friend != null)
                return true;
            return false;
        }

        public IEnumerable<FriendEntity> GetAllFriends(User user)
        {
            return friendRepository.FindAllByUserId(user.Id);
        }

        public IEnumerable<FriendEntity> GetWouldBeFriends(User user)
        {
            return friendRepository.FindAllByFriendId(user.Id);
        }
    }
}
