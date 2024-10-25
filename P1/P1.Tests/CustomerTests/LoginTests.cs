using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.CustomerTests;

public class LoginTests
{

    [Theory]
    [InlineData(null, "password")] //test for username null
    [InlineData("username", null)] // test for password null
    [InlineData(null, null)] //test for both null
    [InlineData("username2", "password")] //test username is wrong
    [InlineData("username", "password2")] //test password is wrong
    [InlineData("username2", "password2")] //test both username and password are wrong
    public void LoginThrowsException(string username, string password)
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [new Customer {Username = "username", Password = "password"}];
        
        LogInDTO logInDTO = new() { Username = username, Password = password}; 
        Customer customer = new() { Username = username, Password = password};
        
        mockRepo.Setup(repo => repo.GetCustomerByUsernamePassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(cList.FirstOrDefault(c => c.Username == username && c.Password == password));
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.Login(logInDTO));
    }

    [Fact]
    public void LoginSuccessfulTest()
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [new Customer {Username = "username", Password = "password"}];
        
        LogInDTO logInDTO = new() { Username = "username", Password = "password"}; 
        Customer customer = new() { Username = "username", Password = "password"};
        
        mockRepo.Setup(repo => repo.GetCustomerByUsernamePassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(cList.FirstOrDefault(c => c.Username == "username" && c.Password == "password"));
        
        //Act
        var result = customerService.Login(logInDTO);
        //Assert
        Assert.NotNull(result);
        Assert.IsType<Customer>(result);
    }
}