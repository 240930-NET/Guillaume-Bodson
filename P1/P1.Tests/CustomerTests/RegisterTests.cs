using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.CustomerTests;

public class RegisterTests
{

    [Theory]
    [InlineData("username", "password")] //test for username already in use
    [InlineData("us", "password")] //test for username too short
    [InlineData("username2", "pas")] //test for password too short
    [InlineData("us", "pas")] //test for both too short
    [InlineData(null, "password")] //test for username null
    [InlineData("username2", null)] // test for password null
    [InlineData(null, null)] //test for both null
    public void RegisterThrowsExceptionTest(string username, string password)
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [new Customer {Username = "username"}];
        
        LogInDTO logInDTO = new() { Username = username, Password = password}; 
        Customer customer = new() { Username = username, Password = password};

        mockRepo.Setup(repo => repo.AddCustomer(It.IsAny<Customer>()))
            .Callback(() => cList.Add(customer))
            .Returns(customer);
        
        mockRepo.Setup(repo => repo.GetCustomerByUsername(It.IsAny<string>()))
            .Returns(cList.FirstOrDefault(c => c.Username == username));
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.Register(logInDTO));
    }

    [Fact]
    public void RegisterAddSuccessfullyTest()
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [new Customer {Username = "username"}];
        
        LogInDTO logInDTO = new() { Username = "username2", Password = "password"}; 
        Customer customer = new() { Username = "username2", Password = "password"};

        mockRepo.Setup(repo => repo.AddCustomer(It.IsAny<Customer>()))
            .Callback(() => cList.Add(customer))
            .Returns(customer);
        
        mockRepo.Setup(repo => repo.GetCustomerByUsername(It.IsAny<string>()))
            .Returns(cList.FirstOrDefault(c => c.Username == "username2"));
        
        //Act
        var result = customerService.Register(logInDTO);
        //Assert
        Assert.NotNull(result);
        Assert.IsType<Customer>(result);
        Assert.Contains(cList, u => u.Username!.Equals("username2"));
        mockRepo.Verify(r => r.AddCustomer(It.IsAny<Customer>()), Times.Exactly(1));
    }
}