using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using DangerSwap.Controllers;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using DangerSwap.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DangerSwap.Test.Controllers;

public sealed class ConverterControllerTest
{
    private readonly Mock<IConverterRepository> _converterRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IScrapperService> _scrapperServiceMock;
    private readonly Mock<ICurrencyService> _currencyServiceMock;
    private readonly Mock<IConverterService> _converterServiceMock;
    private readonly Mock<ICapitalService> _capitalServiceMock;
    private readonly ConverterController _sut;
    private readonly Fixture _fixture;
    public ConverterControllerTest()
    {
        _fixture = new Fixture();
        _converterRepositoryMock = new Mock<IConverterRepository>();
        _scrapperServiceMock = new Mock<IScrapperService>();
        _currencyServiceMock = new Mock<ICurrencyService>();
        _userServiceMock = new Mock<IUserService>();
        _converterServiceMock = new Mock<IConverterService>();
        _capitalServiceMock = new Mock<ICapitalService>();
        _sut = new ConverterController(_converterRepositoryMock.Object,
            _scrapperServiceMock.Object, _currencyServiceMock.Object, _userServiceMock.Object, _converterServiceMock.Object, _capitalServiceMock.Object);
    }

    [Fact]
    public async Task Convert_InvalidModelState_UserServiceNotCalled_ConverterServiceNotCalled()
    {
        // Arrange
        var transactionViewModel = new TransactionViewModel();

        var errorKey = _fixture.Create<string>();
        var errorMessage = _fixture.Create<string>();
        _sut.ModelState.AddModelError(errorKey, errorMessage);

        // Act
        var result = await _sut.Convert(transactionViewModel);

        // Assert
        Assert.NotNull(result);
        _userServiceMock.Verify(_ => _.GetUser(It.IsAny<ClaimsPrincipal>()), Times.Never);
        _converterServiceMock.Verify(_ => _.ConvertCurrency(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async Task Convert_ValidModelState_ServicesAreCalled()
    {
        // Arrange
        var transaction = new Transaction();
        var transactionViewModel = new TransactionViewModel()
        {
            Transaction = transaction,
        };
        var user = new User();
        transaction.User = user;
        var errorKey = _fixture.Create<string>();
        var errorMessage = _fixture.Create<string>();
        _userServiceMock.Setup(_ => _.GetUser(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(user);
        _converterServiceMock.Setup(_ => _.ConvertCurrency(transaction));

        // Act
        var result = await _sut.Convert(transactionViewModel);

        // Assert
        Assert.NotNull(result);
        _userServiceMock.VerifyAll();
        _converterServiceMock.VerifyAll();
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
