using Microsoft.EntityFrameworkCore;
using P1.Models;

namespace P1.Data.Repos;

public class CustomerRepo : ICustomerRepo {
    
    private readonly BankContext _context; // this holds reference to the context

    public CustomerRepo(BankContext context){
        _context = context;
    }

    public List<Customer> GetAllCustomers(){
            return _context.Customers.Include(c => c.Accounts).ToList();        
    }
  
    public Customer? GetCustomerById(int id){
        return _context.Customers
                    .Include(c => c.Accounts)
                    .SingleOrDefault(c => c.CustomerId == id);
    }

    public Customer? GetCustomerByUsername(string username){
         Customer? searchedCustomer = _context.Customers
                                            .Include(c => c.Accounts)
                                            .SingleOrDefault(customer => customer.Username == username);

        if( searchedCustomer != null){
            return searchedCustomer;
        }
        else{
            return null;
        }        
    }
    public Customer? GetCustomerByUsernamePassword(string username, string password){
        Customer? searchedCustomer = _context.Customers
                                .Include(c => c.Accounts)
                                .SingleOrDefault(customer => customer.Username == username
                                && customer.Password == password);
        return searchedCustomer;    
    }

    public Customer AddCustomer(Customer customer){
        _context.Customers.Add(customer);
        _context.SaveChanges(); // to save you data, otherwise it will not persist
        return customer;
    }
    public Customer ChangePassword(Customer customer){
        Customer? existingCustomer = _context.Customers.Find(customer.CustomerId);
        if(existingCustomer != null){
            //Change to new data
            existingCustomer.Password = customer.Password;

            //Save
            _context.SaveChanges();
            return existingCustomer;
        }
        else{
            throw new Exception("The user does not exist");
        }
    }
    public Customer? DeleteCustomer(int id){
        //Find the user in the database
        Customer? searchedCustomer = _context.Customers
                                        .Include(c => c.Accounts)
                                        .SingleOrDefault(c => c.CustomerId == id);
        if( searchedCustomer != null){

            _context.Customers.Remove(searchedCustomer);
            _context.SaveChanges();
            return searchedCustomer;
        }
        else{
            return null;
        }
    }

    public List<Account>? GetAllAccountsById(int id) {
        Customer? searchedCustomer = _context.Customers
                                        .Include(c => c.Accounts)
                                        .SingleOrDefault(c => c.CustomerId == id);
        if (searchedCustomer != null) return searchedCustomer.Accounts;
        return null;
    }
}