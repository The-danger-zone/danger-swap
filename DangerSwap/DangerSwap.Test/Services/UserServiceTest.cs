using AutoFixture;
using DangerSwap.Models;
using DangerSwap.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DangerSwap.Test.Services;

public sealed class UserServiceTest
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly UserService _sut;
    private readonly Fixture _fixture;

    public UserServiceTest()
    {
        _userManagerMock = GetMockUserManager();
        _sut = new UserService(_userManagerMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task UpdatePassword_PasswordIsChanged()
    {
        // Arrange
        var password = _fixture.Create<string>();
        var newPassword = _fixture.Create<string>();
        var user = new User()
        {
            Password = password
        };
        var userInfo = new UpdateableUser()
        {
            OldPassword = password,
            NewPassword = newPassword,
            ConfirmedNewPassword = newPassword,
        };

        // Act
        await _sut.UpdatePassword(userInfo, user);

        // Assert
        _userManagerMock.Verify(_ => _.ChangePasswordAsync(user, userInfo.OldPassword, userInfo.ConfirmedNewPassword), Times.Once);
    }

    [Fact]
    public async Task UpdateEmail_ConfirmedEmailMatchWithOldEmail_EmailIsNotUpdated()
    {
        // Arrange
        var email = _fixture.Create<string>();
        var newEmail = _fixture.Create<string>();
        var token = _fixture.Create<string>();
        var user = new User()
        {
            Email = email
        };
        var userInfo = new UpdateableUser()
        {
            NewEmail = newEmail,
            ConfirmedNewEmail = email,
        };

        // Act
        await _sut.UpdateEmail(userInfo, user);

        // Assert
        _userManagerMock.Verify(_ => _.GenerateChangeEmailTokenAsync(user, userInfo.NewEmail), Times.Never);
        _userManagerMock.Verify(_ => _.ChangeEmailAsync(user, userInfo.NewEmail, token), Times.Never);
    }

    [Fact]
    public async Task UpdateEmail_EmailDoesntMatch_EmailIsNotUpdated()
    {
        // Arrange
        var email = _fixture.Create<string>();
        var newEmail = _fixture.Create<string>();
        var token = _fixture.Create<string>();
        var user = new User()
        {
            Email = email
        };
        var userInfo = new UpdateableUser()
        {
            NewEmail = newEmail,
            ConfirmedNewEmail = "another email",
        };

        // Act
        await _sut.UpdateEmail(userInfo, user);

        // Assert
        _userManagerMock.Verify(_ => _.GenerateChangeEmailTokenAsync(user, userInfo.NewEmail), Times.Never);
        _userManagerMock.Verify(_ => _.ChangeEmailAsync(user, userInfo.NewEmail, token), Times.Never);
    }

    [Fact]
    public async Task UpdateEmail_EmailIsChanged()
    {
        // Arrange
        var email = _fixture.Create<string>();
        var newEmail = _fixture.Create<string>();
        var token = _fixture.Create<string>();
        var user = new User()
        {
            Email = email
        };
        var userInfo = new UpdateableUser()
        {
            NewEmail = newEmail,
            ConfirmedNewEmail = newEmail,
        };
        _userManagerMock.Setup(_ => _.GenerateChangeEmailTokenAsync(user, userInfo.NewEmail))
            .ReturnsAsync(token);
        _userManagerMock.Setup(_ => _.ChangeEmailAsync(user, userInfo.NewEmail, token));
        // Act
        await _sut.UpdateEmail(userInfo, user);

        // Assert
        _userManagerMock.VerifyAll();
    }

    [Fact]
    public async Task UpdatePassword_ConfirmedPasswordDoesntMatch_PasswordIsNotChanged()
    {
        // Arrange
        var password = _fixture.Create<string>();
        var newPassword = _fixture.Create<string>();
        var user = new User()
        {
            Password = password
        };
        var userInfo = new UpdateableUser()
        {
            OldPassword = password,
            NewPassword = newPassword,
            ConfirmedNewPassword = "confirmed new password"
        };

        // Act
        await _sut.UpdatePassword(userInfo, user);

        // Assert
        _userManagerMock.Verify(_ => _.ChangePasswordAsync(user, userInfo.OldPassword, userInfo.ConfirmedNewPassword), Times.Never);
    }

    [Fact]
    public async Task UpdatePassword_PasswordDoesntMatch_PasswordIsNotChanged()
    {
        // Arrange
        var password = _fixture.Create<string>();
        var user = new User()
        {
            Password = password
        };
        var userInfo = new UpdateableUser()
        {
            OldPassword = "another password"
        };

        // Act
         await _sut.UpdatePassword(userInfo, user);

        // Assert
        _userManagerMock.Verify(_ => _.ChangePasswordAsync(user, userInfo.OldPassword, userInfo.ConfirmedNewPassword), Times.Never);
    }

    [Fact]
    public async Task GetUser_ReturnsExistingUser()
    {
        // Arrange
        var user = new User();
        var claim = _fixture.Create<ClaimsPrincipal>();
        _userManagerMock.Setup(_ => _.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(user);
        // Act
        var result = await _sut.GetUser(claim);

        // Assert
        Assert.Equal(user, result);
    }

    private Mock<UserManager<User>> GetMockUserManager()
    {
        var userStoreMock = new Mock<IUserStore<User>>();
        return new Mock<UserManager<User>>(
        userStoreMock.Object, null, null, null, null, null, null, null, null);
    }
}
