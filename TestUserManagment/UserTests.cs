using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using UserRetention.DataBase;
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

        }

    }
}
