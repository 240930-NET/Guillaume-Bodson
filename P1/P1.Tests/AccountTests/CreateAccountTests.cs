using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.AccountTests;

public class CreateAccountTests
{
    [Theory]
    [InlineData("checking", 1)] //Test name taken
    [InlineData(null, 1)] //Test null name 
    public void CreateAccountThrowsExceptionTest(string name, int customerId)
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
            new Account{Name = "checking", CustomerId = 1}
        ];

        CreateAccountDTO createAccountDTO = new() { Name = name, CustomerId = customerId};
        Account account = new() { Name = name, CustomerId = customerId};

        mockRepo.Setup(repo => repo.AddAccount(It.IsAny<Account>()))
            .Callback(() => aList.Add(account))
            .Returns(account);
        
        mockRepo.Setup(repo => repo.CheckNameNotTaken(It.IsAny<Account>()))
            .Returns(aList.FirstOrDefault(c => c.Name == name && c.CustomerId == customerId) == null);
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => accountService.CreateAccount(createAccountDTO));
    }

    [Fact]
    public void CreateAccountThrowsExceptionWhenCustomerIdHasNoRelatedCustomer()
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
            new Account{Name = "checking", CustomerId = 1}
        ];

        CreateAccountDTO createAccountDTO = new() { Name = "checking", CustomerId = 2};
        Account account = new() { Name = "checking", CustomerId = 2};

        //mock the repo throwing an exception when there isn't a customer tied to the customer Id provided
        mockRepo.Setup(repo => repo.AddAccount(It.IsAny<Account>()))
            .Throws<Exception>();
        
        mockRepo.Setup(repo => repo.CheckNameNotTaken(It.IsAny<Account>()))
            .Returns(aList.FirstOrDefault(c => c.Name == "checking" && c.CustomerId == 2) == null);
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => accountService.CreateAccount(createAccountDTO));
    }

    [Theory] //Test customer id doesn't exist
    [InlineData("saving", 1)] //Test different name 
    [InlineData("checking", 2)] //Test different name
    public void CreateAccountAddsNewAccountTest(string name, int customerId)
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
            new Account{Name = "checking", CustomerId = 1}
        ];

        CreateAccountDTO createAccountDTO = new() { Name = name, CustomerId = customerId};
        Account account = new() { Name = name, CustomerId = customerId};

        mockRepo.Setup(repo => repo.AddAccount(It.IsAny<Account>()))
            .Callback(() => aList.Add(account))
            .Returns(account);
        
        mockRepo.Setup(repo => repo.CheckNameNotTaken(It.IsAny<Account>()))
            .Returns(aList.FirstOrDefault(c => c.Name == name && c.CustomerId == customerId) == null);
        
        //Act
        var result = accountService.CreateAccount(createAccountDTO);
        //Assert
         Assert.NotNull(result);
        Assert.IsType<Account>(result);
        Assert.Contains(aList, a => a.Name!.Equals(name));
        mockRepo.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Exactly(1));
    }

}