using AutoFixture;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using DangerSwap.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DangerSwap.Test.Services;

public sealed class ConverterServiceTest
{
    private readonly Mock<IConverterRepository> _converterRepositoryMock;
    private readonly ConverterService _sut;
    private readonly Fixture _fixture;

    public ConverterServiceTest()
    {
        _converterRepositoryMock = new Mock<IConverterRepository>();
        _sut = new ConverterService(_converterRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetCurrencyAsync_CallsService_ReturnsConvertedEquivalent()
    {
        // Arrange
        var transaction = new Transaction();
        var convertedEquivalent = _fixture.Create<decimal>();
        _converterRepositoryMock.Setup(_ => _.CreateTransaction(transaction));
        _converterRepositoryMock.Setup(_ => _.Convert(transaction))
            .Returns(convertedEquivalent);

        // Act
        var result = await _sut.ConvertCurrency(transaction);

        // Assert
        Assert.Equal(convertedEquivalent, result);
        _converterRepositoryMock.VerifyAll();
    }
}
