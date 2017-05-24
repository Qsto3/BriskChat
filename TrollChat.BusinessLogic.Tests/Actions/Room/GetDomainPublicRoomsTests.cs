﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using TrollChat.DataAccess.Repositories.Interfaces;
using Moq;
using TrollChat.BusinessLogic.Actions.Room.Implementations;

namespace TrollChat.BusinessLogic.Tests.Actions.Room
{
    public class GetDomainPublicRoomsTests
    {
        [Fact]
        public void Invoke_ValidData_EditAndSaveAreCalled()
        {
            // prepare
            var roomId = Guid.NewGuid();

            var roomsInDomain = new List<DataAccess.Models.Room>
            {
                new DataAccess.Models.Room
                {
                    Id = roomId,
                    Name = "TestRoom"
                },
                new DataAccess.Models.Room
                {
                    Name = "TestRoom2"
                }
            };

            var mockedRoomRepository = new Mock<IRoomRepository>();
            mockedRoomRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.Room, bool>>>()))
                .Returns(roomsInDomain.AsQueryable());

            var action = new GetDomainPublicRooms(mockedRoomRepository.Object);

            // action
            var result = action.Invoke(Guid.NewGuid());

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("TestRoom", result[0].Name);
            Assert.Equal("TestRoom2", result[1].Name);
            mockedRoomRepository.Verify(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.Room, bool>>>()), Times.Once);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsNull()
        {
            // prepare
            var mockedRoomRepository = new Mock<IRoomRepository>();

            var action = new GetDomainPublicRooms(mockedRoomRepository.Object);

            // action
            var result = action.Invoke(Guid.NewGuid());

            // assert
            Assert.Equal(0, result.Count);
            mockedRoomRepository.Verify(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.Room, bool>>>()), Times.Once);
        }

        [Fact]
        public void Invoke_EmptyGuid_ReturnsNull()
        {
            // prepare
            var mockedRoomRepository = new Mock<IRoomRepository>();

            var action = new GetDomainPublicRooms(mockedRoomRepository.Object);

            // action
            var result = action.Invoke(new Guid());

            // assert
            Assert.Null(result);
            mockedRoomRepository.Verify(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.Room, bool>>>()), Times.Never);
        }
    }
}