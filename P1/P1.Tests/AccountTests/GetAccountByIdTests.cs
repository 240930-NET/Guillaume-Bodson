using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;

namespace P1.Tests.AccountTests;

public class GetAccountByIdTest
{
    
    [Fact]
    public void GetAccountByIdThrowsExceptionTest()
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
        int id = 1;

        mockRepo.Setup(repo => repo.GetAccountById(It.IsAny<int>()))
            .Returns(aList.FirstOrDefault(a => a.AccountId == id));

        //Act
        //Assert
        Assert.Throws<Exception>(() => accountService.GetAccountById(id));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetAccountByIdReturnsTest(int id)
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
            new Account{AccountId = 1},
            new Account{AccountId = 2},
            new Account{AccountId = 3}
        ];

        mockRepo.Setup(repo => repo.GetAccountById(It.IsAny<int>()))
            .Returns(aList.FirstOrDefault(a => a.AccountId == id));

        //Act
        var result = accountService.GetAccountById(id);
 
        //Assert
        Assert.NotNull(result);
        Assert.IsType<Account>(result);
        Assert.Equal(id, result.AccountId);
    }
}