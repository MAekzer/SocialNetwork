using Moq;
using NUnit.Framework;
using Social_Network.DAL.Entities;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class FriendServiceTests
    {
        Mock<IUserRepository> mockUserRepository = new();
        Mock<IFriendRepository> mockFriendRepository = new();

        List<FriendEntity> fakeFriendRepository = new List<FriendEntity>()
        {
            new FriendEntity {id = 0, user_id = 0, friend_id = 0},
            new FriendEntity {id = 1, user_id = 0, friend_id = 1}
        };

        List<UserEntity> fakeUserRepository = new List<UserEntity>()
        {
            new UserEntity {id = 0, firstname = "Jack", lastname = "Daniels", password = "11112222", email = "gmail@gmail.com"},
            new UserEntity {id = 1, firstname = "John", lastname = "Johanson", password = "11112222", email = "mail@gmail.com"}
        };

        public FriendService Preparation(
            out FriendRequest request1,
            out FriendRequest request2,
            out FriendRequest nullRequest)
        {
            mockUserRepository.Setup(x => x.FindByEmail("gmail@gmail.com")).Returns(fakeUserRepository[0]);
            mockUserRepository.Setup(x => x.FindByEmail("mail@gmail.com")).Returns(fakeUserRepository[1]);

            request1 = new()
            {
                User_Id = 0,
                Friend_Email = "mail@gmail.com"
            };
            request2 = new()
            {
                User_Id = 1,
                Friend_Email = "gmail@gmail.com"
            };
            nullRequest = new()
            {
                User_Id = 0,
                Friend_Email = "g"
            };

            IEnumerable<FriendEntity>? fakeFriends1 = fakeFriendRepository;
            IEnumerable<FriendEntity>? fakeFriends2 = from fakefriend in fakeFriendRepository
                                                      where fakefriend.user_id == 1
                                                      select fakefriend;

            mockFriendRepository.Setup(x => x.FindAllByUserId(0)).Returns(fakeFriends1);
            mockFriendRepository.Setup(x => x.FindAllByUserId(1)).Returns(fakeFriends2);

            return new FriendService(mockUserRepository.Object, mockFriendRepository.Object);
        }

        [Test]
        public void IsFriendMustReturnTrueIfUserIsFriend()
        {
            var friendService = Preparation(
                out FriendRequest request1,
                out _,
                out _);

            bool isfriend = friendService.IsFriend(request1, out _);
            Assert.True(isfriend);
        }

        [Test]
        public void IsFriendMustReturnFalseIfUserIsNotFriend()
        {
            var friendService = Preparation(
                out _,
                out FriendRequest request2,
                out _);

            bool isnotfriend = friendService.IsFriend(request2, out _);
            Assert.False(isnotfriend);
        }

        [Test]
        public void IsFriendMustGiveOutCorrectParametr()
        {
            var friendService = Preparation(
            out FriendRequest request1,
            out _,
            out _);

            friendService.IsFriend(request1, out FriendEntity friend);
            Assert.True(friend == fakeFriendRepository[1]);
        }

        [Test]
        public void IsFriendMustGiveOutNullIfFriendNotFound()
        {
            var friendService = Preparation(
            out _,
            out FriendRequest request2,
            out _);

            friendService.IsFriend(request2, out FriendEntity friend);
            Assert.True(friend is null);
        }

        [Test]
        public void IsFriendMustThrowUserNotFoundExceptionIfEmailIsIncorrect()
        {
            var friendService = Preparation(
            out _,
            out _,
            out FriendRequest nullRequest);

            Assert.Throws<UserNotFoundException>(() => friendService.IsFriend(nullRequest, out FriendEntity friend));
        }
    }
}