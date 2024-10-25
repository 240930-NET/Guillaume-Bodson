using AutoMapper;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.CustomerTests;

public class GetAllCustomerAccountsByIdTests
{
    [Fact]
    public void GetAllAccountsByIdThrowsException() 
    {
         Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        Customer customer =  new Customer {CustomerId = 1, 
            Accounts = [new Account{Balance = 0}]
        };

        int id = 2;

        List<Customer> cList = [customer];
        
        mockRepo.Setup(repo => repo.GetAllAccountsById(It.IsAny<int>()))
            .Returns(cList.SingleOrDefault(c => c.CustomerId == id)?.Accounts);

        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.GetAllAccountsById(id));
    }

    [Fact]
    public void GetAllAccountsByIdGetsAccountsTest() 
    {
         Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        Customer customer =  new Customer {CustomerId = 1, 
            Accounts = [
                new Account{Balance = 0},
                new Account{Balance = 50},
                new Account{Balance = 25.55},
            ]
        };
        
        int id = 1;

        List<Customer> cList = [customer];
        
        mockRepo.Setup(repo => repo.GetAllAccountsById(It.IsAny<int>()))
            .Returns(cList.SingleOrDefault(c => c.CustomerId == id)?.Accounts);

        //Act
        var result = customerService.GetAllAccountsById(id);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(customer.Accounts, result);
    }

    [Fact]
    public void GetAllAccountsByIdReturnsEmptyListTest() 
    {
         Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        Customer customer =  new Customer {CustomerId = 1};
        
        int id = 1;

        List<Customer> cList = [customer];
        
        mockRepo.Setup(repo => repo.GetAllAccountsById(It.IsAny<int>()))
            .Returns(cList.SingleOrDefault(c => c.CustomerId == id)?.Accounts);

        //Act
        var result = customerService.GetAllAccountsById(id);
        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

}