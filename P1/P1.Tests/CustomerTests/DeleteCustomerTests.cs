using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Moq;
using P1.API.Services;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.Tests.CustomerTests;

public class DeleteCustomerTests
{
    [Fact] 
    public void DeleteWhileAccountsNotEmptyThrowsExceptionTest()
    {
        //Arrange
        Mock<ICustomerRepo> mockRepo = new();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        CustomerService customerService = new(mockRepo.Object, mapper);

        Customer customer =  new Customer {CustomerId = 1, 
            Accounts = [new Account{Balance = 50}]
        };

        List<Customer> cList = [customer];
        
        int id = 1;
        

        mockRepo.Setup(repo => repo.GetAllAccountsById(It.IsAny<int>()))
            .Returns(cList.First(c => c.CustomerId == id).Accounts);

        mockRepo.Setup(repo => repo.DeleteCustomer(It.IsAny<int>()))
            .Callback(() => cList.Remove(customer))
            .Returns(customer);
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.DeleteCustomer(id));
    }

    [Fact] 
    public void DeleteAccountThatDoesntExistThrowsExceptionTest()
    {
        //Arrange
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

        List<Customer> cList = [customer];
        
        int id = 2;
        

        mockRepo.Setup(repo => repo.GetAllAccountsById(It.IsAny<int>()))
            .Returns(cList.SingleOrDefault(c => c.CustomerId == id)?.Accounts);

        mockRepo.Setup(repo => repo.DeleteCustomer(It.IsAny<int>()))
            .Callback(() => cList.Remove(customer))
            .Returns(customer);
        
        //Act
        //Assert
        Assert.Throws<Exception>(() => customerService.DeleteCustomer(id));
    }

    [Fact] 
    public void DeleteAccountGetsDeletedTest()
    {
        //Arrange
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

        List<Customer> cList = [customer];
        
        int id = 1;
        

        mockRepo.Setup(repo => repo.GetAllAccountsById(It.IsAny<int>()))
            .Returns(cList.First(c => c.CustomerId == id).Accounts);

        mockRepo.Setup(repo => repo.DeleteCustomer(It.IsAny<int>()))
            .Callback(() => cList.Remove(customer))
            .Returns(customer);
        
        //Act
        var result = customerService.DeleteCustomer(id);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(customer, result);
        Assert.DoesNotContain(cList, c => c.CustomerId.Equals(id));
    }
}