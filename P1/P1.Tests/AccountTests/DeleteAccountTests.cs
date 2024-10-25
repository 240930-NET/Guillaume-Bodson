using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;

namespace P1.Tests.AccountTests;

public class DeleteAccountTests
{   
    [Theory]
    [InlineData(1)] //Tests Account isn't empty
    [InlineData(2)] //Tests Account doesnt exist
    public void DeleteThrowsExceptionTest(int id)
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
            new Account{AccountId = 1, Balance = 100}
        ]; 

        Account account = new() { AccountId = 1, Balance = 100};

        mockRepo.Setup(repo => repo.DeleteAccount(It.IsAny<int>()))
            .Returns(account);

        mockRepo.Setup(repo => repo.GetAccountById(It.IsAny<int>()))
            .Returns(aList.FirstOrDefault(c => c.AccountId == id));

        //Act
        //Assert
         Assert.Throws<Exception>(() => accountService.DeleteAccount(id));
    }

    [Fact] 
    public void DeleteSuccessfullyTest()
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

        int id = 1;

        Account account = new() { AccountId = 1, Balance = 0};

        mockRepo.Setup(repo => repo.DeleteAccount(It.IsAny<int>()))
            .Callback(() => aList.Remove(account))
            .Returns(account);

        mockRepo.Setup(repo => repo.GetAccountById(It.IsAny<int>()))
            .Returns(aList.FirstOrDefault(c => c.AccountId == id));

        //Act
        var result = accountService.DeleteAccount(id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(account, result);
    }
}