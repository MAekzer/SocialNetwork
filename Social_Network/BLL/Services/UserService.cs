using Social_Network.DAL.Entities;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook
            };

            if (this.userRepository.Update(updatableUserEntity) == 0)
                throw new Exception();
        }

        public User ConstructUserModel(UserEntity userEntity)
        {
            return new User(userEntity.id,
                          userEntity.firstname,
                          userEntity.lastname,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book);
        }

        public void Register(UserRegistrationData userData)
        {
            if (String.IsNullOrEmpty(userData.FirstName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userData.LastName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userData.Email))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userData.Password))
                throw new ArgumentNullException();

            if (userData.Password.Length < 8)
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(userData.Email))
                throw new ArgumentNullException();

            if (userRepository.FindByEmail(userData.Email) != null)
                throw new ArgumentNullException();

            UserEntity userEntity = new()
            {
                firstname = userData.FirstName,
                lastname = userData.LastName,
                password = userData.Password,
                email = userData.Email
            };

            if (userRepository.Create(userEntity) == 0)
                throw new Exception();
        }
    }
}
