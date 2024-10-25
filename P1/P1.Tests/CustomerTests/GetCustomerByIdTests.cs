using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;

namespace P1.Tests.CustomerTests;

public class GetCustomerByIdTest
{

    [Fact]
    public void GetCustomerByIdThrowsExceptionTest()
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

        int id = 1;

        mockRepo.Setup(repo => repo.GetCustomerById(It.IsAny<int>()))
            .Returns(cList.FirstOrDefault(c => c.CustomerId == id));
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.GetCustomerById(id));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetCustomerByIdReturns(int id)
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
            new Customer {CustomerId = 1},
            new Customer {CustomerId = 2},
            new Customer {CustomerId = 3}
        ];

        mockRepo.Setup(repo => repo.GetCustomerById(It.IsAny<int>()))
            .Returns(cList.FirstOrDefault(c => c.CustomerId == id));
        
        //Act
         var result = customerService.GetCustomerById(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Customer>(result);
        Assert.Equal(id, result.CustomerId);
    }
}