using System;
using System.Threading.Tasks;
using AutoFixture;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using DangerSwap.Services;
using Moq;
using Xunit;

namespace DangerSwap.Test.Services;

public sealed class CurrencyServiceTest
{
    private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
    private readonly Mock<IScrapperService> _scrapperServiceMock;
    private readonly ICurrencyService _sut;
    private readonly Fixture _fixture;

    public CurrencyServiceTest()
    {
        _currencyRepositoryMock = new Mock<ICurrencyRepository>();
        _scrapperServiceMock = new Mock<IScrapperService>();
        _sut = new CurrencyService(_currencyRepositoryMock.Object, _scrapperServiceMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetCurrencyAsync_CallsRepository()
    {
        // Arrange
        var currencyId = _fixture.Create<string>();
        var guid = new Guid(currencyId);
        var currency = _fixture.Create<Currency>();
        _currencyRepositoryMock.Setup(_ => _.GetEntity(guid))
            .ReturnsAsync(currency);
        // Act
        var result = await _sut.GetCurrencyAsync(currencyId);

        // Assert
        Assert.Equal(currency, result);
        _currencyRepositoryMock.VerifyAll();
    }
}