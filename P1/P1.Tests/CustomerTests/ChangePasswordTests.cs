using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.CustomerTests;

public class ChangePasswordTest
{
    [Theory]
    [InlineData("username", "password", "password")] //test password is the same
    [InlineData(null, "password", "password2")] // test for username null
    [InlineData("username", null, "password2")] //test old password null
    [InlineData("username", "password", null)] //test new password null
    [InlineData("username", "password2", "password3")] //test for current username password combination doesnt exist
    [InlineData("username", "password", "pas")] //test for new password too short
    public void ChangePasswordThrowsException(string username, string oldPassword, string newPassword)
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
        
        PasswordChangeDTO passwordChangeDTO = new() { Username = username, OldPassword = oldPassword, NewPassword = newPassword}; 
        Customer newCustomer = new() { Username = username, Password = newPassword};
        
        mockRepo.Setup(repo => repo.ChangePassword(It.IsAny<Customer>()))
            .Returns(newCustomer);

        mockRepo.Setup(repo => repo.GetCustomerByUsernamePassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(cList.FirstOrDefault(c => c.Username == username && c.Password == oldPassword));
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.ChangePassword(passwordChangeDTO));
    }

    [Fact]
    public void ChangePasswordChangesPasswordTest() 
    {
         Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [new Customer {Username = "username", Password = "password"}];
        
        PasswordChangeDTO passwordChangeDTO = new() { Username = "username", OldPassword = "password", NewPassword = "password2"}; 
        Customer newCustomer = new() { Username = "username", Password = "password2"};
        
        mockRepo.Setup(repo => repo.ChangePassword(It.IsAny<Customer>()))
            .Returns(newCustomer);

        mockRepo.Setup(repo => repo.GetCustomerByUsernamePassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(cList.FirstOrDefault(c => c.Username == "username" && c.Password == "password"));

        //Act
        var result = customerService.ChangePassword(passwordChangeDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("password2", result.Password);
    }
}