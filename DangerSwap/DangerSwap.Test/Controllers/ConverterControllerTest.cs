using System.Threading.Tasks;
using AutoFixture;
using DangerSwap.Controllers;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DangerSwap.Test.Controllers;

public sealed class ConverterControllerTest
{
    private readonly Mock<IConverterRepository> _converterRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IScrapperService> _scrapperServiceMock;
    private readonly Mock<ICurrencyService> _currencyServiceMock;
    private readonly ConverterController _sut;
    private readonly Fixture _fixture;
    public ConverterControllerTest()
    {
        _fixture = new Fixture();
        _converterRepositoryMock = new Mock<IConverterRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _scrapperServiceMock = new Mock<IScrapperService>();
        _currencyServiceMock = new Mock<ICurrencyService>();
        _sut = new ConverterController(_converterRepositoryMock.Object, _userRepositoryMock.Object,
            _scrapperServiceMock.Object, _currencyServiceMock.Object);
    }

    [Fact]
    public async Task GetCurrencyInformation_CallsCurrencyService_ReturnsOk()
    {
        // Arrange
        var currencyId = _fixture.Create<string>();
        var currency = _fixture.Create<Currency>();
        _currencyServiceMock.Setup(_ => _.GetCurrencyAsync(currencyId))
            .ReturnsAsync(currency);

        // Act
        var result = await _sut.GetCurrencyInformation(currencyId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _currencyServiceMock.VerifyAll();
    }
}
