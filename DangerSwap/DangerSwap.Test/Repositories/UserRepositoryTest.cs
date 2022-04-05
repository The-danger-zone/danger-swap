using DangerSwap.DbContexts;
using DangerSwap.Models;
using DangerSwap.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DangerSwap.Test.DbContext
{
    public class UserRepositoryTest
    {
        private UserRepository _userRepository { get; set; }
        private readonly DbContextOptions<DangerSwapContext> _options;
        private const string _dataSource = ":memory:";

        public UserRepositoryTest()
        {
            var connection = GetSqliteConnection();
            _options = new DbContextOptionsBuilder<DangerSwapContext>()
            .UseSqlite(connection)
            .Options;

            var userManagerMock = CreateUserManagerMock();
            userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userRepository = new UserRepository(userManagerMock.Object, new DangerSwapContext(_options));
        }

        private SqliteConnection GetSqliteConnection()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = _dataSource };
            return new SqliteConnection(connectionStringBuilder.ToString());
        }

        private Mock<UserManager<User>> CreateUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task RemoveUser_CorrectId_UserSuccessfullyRemoved()
        {
            // Arrange
            using var context = new DangerSwapContext(_options);
            PrepareInMemoryDatabase(context);
            await CreateDummyUsers(context);

            //Act
            int initialNumberOfUsers = await context.Users.CountAsync();
            var user = await context.Users.FirstAsync();
            await _userRepository.DeleteEntity(user.Id);
            int result = await context.Users.CountAsync();

            //Assert
            Assert.Equal(initialNumberOfUsers - 1, result);
        }

        [Fact]
        public async Task RemoveUser_WrongId_UserSuccessfullyRemoved()
        {
            // Arrange
            using var context = new DangerSwapContext(_options);
            PrepareInMemoryDatabase(context);
            await CreateDummyUsers(context);

            //Act
            int initialNumberOfUsers = await context.Users.CountAsync();
            var user = await context.Users.FirstAsync();
            Task action() =>  _userRepository.DeleteEntity(string.Empty);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
            int result = await context.Users.CountAsync();
            Assert.Equal(initialNumberOfUsers, result);
        }

        [Fact]
        public async Task IsAny_CorrectData_UserSuccessfullyFound()
        {
            // Arrange
            using var context = new DangerSwapContext(_options);
            PrepareInMemoryDatabase(context);
            await CreateDummyUsers(context);
            bool result = true;

            //Act 
            var users = await context.Users.ToListAsync();
            foreach(var user in users)
            {
                if (!result)
                    break;
                result = await _userRepository.IsExist(user.Id);
            }

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsAny_WrongId_UserNotFound()
        {
            // Arrange
            using var context = new DangerSwapContext(_options);
            PrepareInMemoryDatabase(context);
            bool result = true;

            //Act 
                result = await _userRepository.IsExist("");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetEntity_EmailAndPassword_SuccessfullyFound()
        {
            // Arrange
            using var context = new DangerSwapContext(_options);
            PrepareInMemoryDatabase(context);
            await CreateDummyUsers(context);
            var results = new Dictionary<string[], User?>();

            // Act
            var users = await context.Users.ToListAsync();
            foreach(var user in users)
            {
                var entity = await _userRepository.GetEntity(user.Email, user.Password);
                results.Add(new string[] { user.Email, user.Password }, entity);
            }

            // Assert
            foreach(var result in results)
            {
                Assert.NotNull(result.Value);
                Assert.Equal(result.Key[0], result.Value?.Email);
                Assert.Equal(result.Key[1], result.Value?.Password);
            }

        }

        private static IEnumerable<object[]> UsersData()
        {
            yield return new object[] {
                new User
                {
                    UserName = "JohnDoe",
                    Email = "newjohn@doe",
                    Citizenship = "Unknown",
                    Nationality = "Unknown",
                    Password = "asdFGH123!@#$",
                    BirthDate = DateTime.Now,
                }
            };
            yield return new object[] {
                new User
                {
                    UserName = "JaneDoe",
                    Email = "jane@doe",
                    Citizenship = "Lithuanian",
                    Nationality = "Unknown",
                    Password = "asdFGH123!@#$",
                    BirthDate = DateTime.Now,
                }
            };
            yield return new object[] {
                new User
                {
                    UserName = "RickDoe",
                    Email = "john@doe",
                    Citizenship = "Unknown",
                    Nationality = "Bulgarian",
                    Password = "asdFGH123!@#$",
                    BirthDate = DateTime.Now,
                }
            };
            yield return new object[] {
                new User
                {
                    UserName = "JohnDoe",
                    Email = "john@doe",
                    Citizenship = "Unknown",
                    Nationality = "Unknown",
                    Password = "newInteristingp$w3",
                    BirthDate = DateTime.Now,
                }
            };
        }

        private async Task CreateDummyUsers(DangerSwapContext context)
        {
            var fakeUsers = GetDummyUsers();
            await context.AddRangeAsync(fakeUsers);
            await context.SaveChangesAsync();
        }

        private IEnumerable<User> GetDummyUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    UserName = "JohnDoe",
                    Email = "john@doe",
                    Citizenship = "Unknown",
                    Nationality = "Unknown",
                    Password = "asdFGH123!@#$",
                    BirthDate = DateTime.Now,
                },
                new User()
                {
                    UserName = "JaneDoe",
                    Email = "jane@doe",
                    Citizenship = "Unknown",
                    Nationality = "Unknown",
                    Password = "asdFGH123!@#$",
                    BirthDate = DateTime.Now,
                },
                new User()
                {
                    UserName = "RickDoe",
                    Email = "rick@doe",
                    Citizenship = "Unknown",
                    Nationality = "Unknown",
                    Password = "asdFGH123!@#$",
                    BirthDate = DateTime.Now
                },
            };
            return users;
        }

        private void PrepareInMemoryDatabase(DangerSwapContext context)
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }

    }
}
