using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;

namespace P1.Tests.CustomerTests;

public class GetAllCustomersTests
{
    
    [Fact]
    public void GetAllCustomersReturnsEmptyListTest()
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [];

        mockRepo.Setup(repo => repo.GetAllCustomers())
            .Returns(cList);

        //Act
        List<Customer> result = customerService.GetAllCustomers();
        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetAllCustomersReturnsNotEmptyListTest()
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        List<Customer> cList = [
            new Customer {Username = "username"},
            new Customer {},
            new Customer {}
        ];

        mockRepo.Setup(repo => repo.GetAllCustomers())
            .Returns(cList);

        //Act
        List<Customer> result = customerService.GetAllCustomers();
        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
        Assert.Contains(result, c => c.Username!.Equals("username"));
    }
}