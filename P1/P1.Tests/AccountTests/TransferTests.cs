using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.AccountTests;

public class TransferTests
{
    [Theory]
    [InlineData(1, 2, -50)] //Test negative amount
    [InlineData(1, 2, 0)] //Test 0 amount
    [InlineData(1, 1, 50)] //Test transfering to same account
    [InlineData(3, 1, 50)] //Test from account doesn't exist
    [InlineData(1, 3, 50)] //Test to account doesn't exist
    [InlineData(1, 2, 1000)] //Test from account doesn't have enough money
    public void TransferThrowsExceptionTest(int fromAccountId, int toAccountId, double amount)
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
            new Account{AccountId = 1, Balance = 100},
            new Account{AccountId = 2, Balance = 0}
        ];

        TransferDTO transferDTO = new() { FromAccountId = fromAccountId, ToAccountId = toAccountId, Amount = amount};

        List<Account> newAccounts = [
            new Account{AccountId = 1, Balance = 100 - amount},
            new Account{AccountId = 2, Balance = 0 + amount}
        ];

        mockRepo.Setup(repo => repo.Transfer(It.IsAny<TransferDTO>()))
            .Returns(newAccounts);
        
        mockRepo.Setup(repo => repo.GetAccountById(fromAccountId))
            .Returns(aList.FirstOrDefault(c => c.AccountId == fromAccountId));
        
        mockRepo.Setup(repo => repo.GetAccountById(toAccountId))
            .Returns(aList.FirstOrDefault(c => c.AccountId == toAccountId));
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => accountService.Transfer(transferDTO));
    }

    [Theory]
    [InlineData(1, 2, 50)] 
    [InlineData(2, 1, 50)]
    public void DepositSuccessfulTest(int fromAccountId, int toAccountId, double amount)
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
            new Account{AccountId = 1, Balance = 100},
            new Account{AccountId = 2, Balance = 100}
        ];

        TransferDTO transferDTO = new() { FromAccountId = fromAccountId, ToAccountId = toAccountId, Amount = amount};

        List<Account> newAccounts = [
            new Account{AccountId = 1, Balance = 100 - amount},
            new Account{AccountId = 2, Balance = 100 + amount}
        ];

        mockRepo.Setup(repo => repo.Transfer(It.IsAny<TransferDTO>()))
            .Returns(newAccounts);
        
        mockRepo.Setup(repo => repo.GetAccountById(fromAccountId))
            .Returns(aList.FirstOrDefault(c => c.AccountId == fromAccountId));
        
        mockRepo.Setup(repo => repo.GetAccountById(toAccountId))
            .Returns(aList.FirstOrDefault(c => c.AccountId == toAccountId));
        
        //Act
        var result = accountService.Transfer(transferDTO);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(newAccounts, result);
    }
}