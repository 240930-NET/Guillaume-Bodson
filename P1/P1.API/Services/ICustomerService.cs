using P1.Models;
using P1.Models.DTO;

namespace P1.API.Services;

public interface ICustomerService {

    
    // Get all Customers
    public List<Customer> GetAllCustomers();

    // Get Customer by Id
    public Customer GetCustomerById(int id);

    // Add new Customer / Register
    public Customer Register(LogInDTO loginDTO);

    public Customer Login(LogInDTO loginDTO);

    //Edit Password
    public Customer ChangePassword(PasswordChangeDTO passwordChangeDTO);

    // Delete Customer
    public Customer DeleteCustomer(int id);

    public List<Account> GetAllAccountsById(int id);
}