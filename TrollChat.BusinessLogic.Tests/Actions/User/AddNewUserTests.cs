﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using TrollChat.BusinessLogic.Actions.User.Implementation;
using TrollChat.BusinessLogic.Helpers.Interfaces;
using TrollChat.DataAccess.Repositories.Interfaces;
using Xunit;

//TODO: Update tests to use tokens
namespace TrollChat.BusinessLogic.Tests.Actions.User
{
    public class AddNewUserTests
    {
        [Fact]
        public void Invoke_ValidData_AddsUserToDatabaseWithCorrectValues()
        {
            // prepare
            var userData = new Models.UserModel
            {
                Email = "email",
                Password = "plain",
                Name = "Ryszard"
            };
            DataAccess.Models.User userSaved = null;

            var mockedUserTokenRepository = new Mock<IUserTokenRepository>();

            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.Add(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(u => userSaved = u);

            var mockedHasher = new Mock<IHasher>();
            mockedHasher.Setup(h => h.GenerateRandomSalt()).Returns("salt-generated");
            mockedHasher.Setup(h => h.CreatePasswordHash("plain", "salt-generated")).Returns("plain-hashed");

            var action = new AddNewUser(mockedUserRepo.Object, mockedUserTokenRepository.Object, mockedHasher.Object);

            // action
            action.Invoke(userData);

            // assert
            Assert.Equal("plain-hashed", userSaved.PasswordHash);
            Assert.Equal("salt-generated", userSaved.PasswordSalt);
            Assert.Equal("Ryszard", userSaved.Name);
            mockedUserRepo.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Once());
            mockedUserRepo.Verify(r => r.Save(), Times.Exactly(2));
        }

        [Fact]
        public void Invoke_ValidData_AddAndSaveAreCalled()
        {
            // prepare
            var userToAdd = new Models.UserModel { Email = "test@test.pl", Password = "test", Name = "Tester" };
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserTokenRepository = new Mock<IUserTokenRepository>();

            var action = new AddNewUser(mockedUserRepository.Object, mockedUserTokenRepository.Object);

            // action
            action.Invoke(userToAdd);

            // assert
            mockedUserRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Once());
            mockedUserRepository.Verify(r => r.Save(), Times.Exactly(2));
        }

        [Fact]
        public void Invoke_InvalidData_AddNorSaveAreCalled()
        {
            // prepare
            var userToAdd = new Models.UserModel();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserTokenRepository = new Mock<IUserTokenRepository>();

            var action = new AddNewUser(mockedUserRepository.Object, mockedUserTokenRepository.Object);

            // action
            var actionResult = action.Invoke(userToAdd);

            // assert
            Assert.Equal(null, actionResult);
            mockedUserRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_AlreadyExists_AddNorSaveAreCalled()
        {
            // prepare
            var userToAdd = new Models.UserModel
            {
                Email = "test",
                Password = "Password"
            };
            var userFromDb = new DataAccess.Models.User
            {
                Email = "test",
            };
            var findByResult = new List<DataAccess.Models.User> { userFromDb };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserTokenRepository = new Mock<IUserTokenRepository>();

            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable());

            var action = new AddNewUser(mockedUserRepository.Object, mockedUserTokenRepository.Object);

            // action
            var actionResult = action.Invoke(userToAdd);

            // assert
            Assert.Equal(null, actionResult);
            mockedUserRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}