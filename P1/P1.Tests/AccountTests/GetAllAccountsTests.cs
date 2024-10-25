using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;

namespace P1.Tests.AccountTests;

public class GetAllAccountsTests
{
    
    [Fact]
    public void GetAllCustomersReturnsEmptyListTest()
    {
        //Arrange
        Mock<IAccountRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        AccountService accountService = new(mockRepo.Object, mapper);

        List<Account> aList = [];

        mockRepo.Setup(repo => repo.GetAllAccounts())
            .Returns(aList);

        //Act
        List<Account> result = accountService.GetAllAccounts();
        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetAllAccountsReturnsNotEmptyListTest()
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
            new Account{Name = "checking"},
            new Account{},
            new Account{}
        ];

        mockRepo.Setup(repo => repo.GetAllAccounts())
            .Returns(aList);

        //Act
        List<Account> result = accountService.GetAllAccounts();
 
        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
        Assert.Contains(result, a => a.Name!.Equals("checking"));
    }
}