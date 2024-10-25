using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.AccountTests;

public class DepositTests
{
    [Theory]
    [InlineData(1, -50)] //Test negative amount
    [InlineData(1, 0)] //Test 0 amount
    [InlineData(2, 50)] //Test account doesn't exist
    public void DepositThrowsExceptionTest(int accountId, double amount)
    {
        //Arrange
        Mock<IAccountRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        AccountService accountService = new(mockRepo.Object, mapper);

        List<Account> aList = [
            new Account{AccountId = 1, Balance = 0}
        ];

        DepositWithdrawDTO depositWithdrawDTO = new() { AccountId = accountId, Amount = amount};
        Account newAccount = new() {AccountId = 1, Balance = 0 + amount};

        mockRepo.Setup(repo => repo.Deposit(It.IsAny<DepositWithdrawDTO>()))
            .Returns(newAccount);
        
        mockRepo.Setup(repo => repo.GetAccountById(It.IsAny<int>()))
            .Returns(aList.FirstOrDefault(c => c.AccountId == accountId));
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => accountService.Deposit(depositWithdrawDTO));
    }

    [Theory]
    [InlineData(1, 50)] 
    [InlineData(2, 200)]
    public void DepositSuccessfulTest(int accountId, double amount)
    {
        //Arrange
        Mock<IAccountRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        AccountService accountService = new(mockRepo.Object, mapper);

        List<Account> aList = [
            new Account{AccountId = 1, Balance = 0},
            new Account{AccountId = 2, Balance = 0}
        ];

        DepositWithdrawDTO depositWithdrawDTO = new() { AccountId = accountId, Amount = amount};
        Account newAccount = new() {AccountId = accountId, Balance = 0 + amount};

        mockRepo.Setup(repo => repo.Deposit(It.IsAny<DepositWithdrawDTO>()))
            .Returns(newAccount);
        
        mockRepo.Setup(repo => repo.GetAccountById(It.IsAny<int>()))
            .Returns(aList.FirstOrDefault(c => c.AccountId == accountId));
        
        //Act
        var result = accountService.Deposit(depositWithdrawDTO);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(newAccount, result);
    }
}
    