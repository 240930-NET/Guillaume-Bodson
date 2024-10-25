using P1.Models;

namespace P1.Data.Repos;

//Provide all CRUD operations you need to
public interface ICustomerRepo{

    public List<Customer> GetAllCustomers();
    public Customer? GetCustomerById(int id);
    public Customer? GetCustomerByUsername(string username);
    public Customer? GetCustomerByUsernamePassword(string username, string password);
    public Customer AddCustomer(Customer customer);
    public Customer ChangePassword(Customer customer);
    public Customer? DeleteCustomer(int id);
    public List<Account>? GetAllAccountsById(int id);

}