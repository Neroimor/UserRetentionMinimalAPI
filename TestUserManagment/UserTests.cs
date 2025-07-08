using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using UserRetention.DataBase;
using UserRetention.DataBase.DTO;
using UserRetention.Services;

namespace TestUserManagment
{
    public class UserTests
    {
        private readonly UserManagement userManagement;
        private readonly Mock<ILogger<UserManagement>> loggerMock;
        private readonly AppDBContext appDBContextMock;

        public UserTests()
        {
            loggerMock = new Mock<ILogger<UserManagement>>();
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            appDBContextMock = new AppDBContext(options);

            userManagement = new UserManagement(loggerMock.Object, appDBContextMock);

            appDBContextMock.Database.EnsureDeleted();
            appDBContextMock.Database.EnsureCreated();
        }

        public RequestUser CreateTestUserRequest(string name, string email)
        {
            return new RequestUser
            {
                Name = name,
                Email = email
            };
        }



        [Fact]
        public async Task AddUserAsync_ShouldAddUser_WhenUserDoesNotExist()
        {
            var userRequest = CreateTestUserRequest("Test User", "tst@tst.ts");

            var result = await userManagement.AddUserAsync(userRequest);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Test User", result.Data.Name);
            Assert.Equal("tst@tst.ts", result.Data.Email);

        }

        [Fact]
        public async Task TestDeleteUserAsync()
        {
            var userRequest = CreateTestUserRequest("Test User", "tst@tst.ts");
            await userManagement.AddUserAsync(userRequest);
            var result = await userManagement.DeleteUserAsync(userRequest.Email);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);

        }

        [Fact]
        public async Task TestGetUserAsync()
        {
            var userRequest = CreateTestUserRequest("Test User", "tst@tst.ts");
            await userManagement.AddUserAsync(userRequest);
            var result = await userManagement.GetUserAsync(userRequest.Email);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Test User", result.Data.Name);
        }

        [Fact]
        public async Task TestUpdateUserAsync()
        {
            var userRequest = CreateTestUserRequest("Test User", "tst@tst.ts");
            await userManagement.AddUserAsync(userRequest);
            var newUserRequest = CreateTestUserRequest("Updated User", "tst@tst.ts");


            var result = await userManagement.UpdateUserAsync(userRequest.Email, newUserRequest);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Updated User", result.Data.Name);

        }


        [Fact]
        public async Task TestGetAllUsersAsync()
        {
            var userRequest = CreateTestUserRequest("Test User", "tst@tst.ts");
            var userRequest1 = CreateTestUserRequest("Test User1", "tst1@tst1.ts");
            await userManagement.AddUserAsync(userRequest);
            await userManagement.AddUserAsync(userRequest1);

            var result = await userManagement.GetAllUsersAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Name == "Test User" && u.Email == "tst@tst.ts");
            Assert.Contains(result, u => u.Name == "Test User1" && u.Email == "tst1@tst1.ts");





        }
    }
}
