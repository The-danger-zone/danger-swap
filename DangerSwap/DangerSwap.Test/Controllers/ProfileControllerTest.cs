using AutoFixture;
using DangerSwap.Controllers;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace DangerSwap.Test.Controllers;

public sealed class ProfileControllerTest
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ICapitalService> _capitalServiceMock;
    private readonly Mock<IConverterRepository> _converterRepositoryMock;
    private readonly Fixture _fixture;
    private readonly ProfileController _sut;

    public ProfileControllerTest()
    {
        _userServiceMock = new Mock<IUserService>();
        _capitalServiceMock = new Mock<ICapitalService>();
        _converterRepositoryMock = new Mock<IConverterRepository>();
        _fixture = new Fixture();
        _sut = new ProfileController(_userServiceMock.Object, _converterRepositoryMock.Object, _capitalServiceMock.Object);
    }

    [Fact]
    public async Task Update_ValidUserInfo_Redirected()
    {
        // Arrange
        var userInfo = _fixture.Create<UpdateableUser>();

        // Act
        var result = await _sut.Update(userInfo);

        // Assert
        Assert.NotNull(result);
        _userServiceMock.Verify(_ => _.UpdateUserInfo(userInfo, It.IsAny<ClaimsPrincipal>()), Times.Once);
    }

    [Fact]
    public async Task Update_InvalidUserInfo_Redirected()
    {
        // Arrange
        var userInfo = _fixture.Create<UpdateableUser>();
        var userClaim = _fixture.Create<ClaimsPrincipal>();
        var errorKey = _fixture.Create<string>();
        var errorMessage = _fixture.Create<string>();
        _sut.ModelState.AddModelError(errorKey, errorMessage);

        // Act
        var result = await _sut.Update(userInfo);

        // Assert
        Assert.NotNull(result);
        _userServiceMock.Verify(_ => _.UpdateUserInfo(userInfo, It.IsAny<ClaimsPrincipal>()), Times.Never);
    }
}
