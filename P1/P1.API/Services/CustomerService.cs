using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.API.Services;

public class CustomerService : ICustomerService {

    private readonly ICustomerRepo _customerRepo;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepo customerRepo, IMapper mapper){
        _customerRepo =  customerRepo; 
        _mapper = mapper;
    }

    // Get all customers 
    public List<Customer> GetAllCustomers(){

        List<Customer> result = _customerRepo.GetAllCustomers();
        return result;
    }

    // Get Customers by Id
    public Customer GetCustomerById(int id){

        Customer? customer = _customerRepo.GetCustomerById(id);
        if(customer != null){
            return customer;
        }
        else{
            throw new Exception("Customer not found");
        }
    }

    //Gets all Accounts of a customer
    public List<Account> GetAllAccountsById(int id) {
        List<Account>? accounts = _customerRepo.GetAllAccountsById(id);
        if (accounts != null) return accounts;
        throw new Exception("Customer with that id does not exist");
    }

     // Add new Customer (Register)
    public Customer Register(LogInDTO logInDTO){
        var customer = _mapper.Map<Customer>(logInDTO);
        //Change the conditions for password and username
        if(!customer.Username.IsNullOrEmpty() && !customer.Password.IsNullOrEmpty()){
            if(customer.Username!.Length < 3) {
                throw new Exception("Username too short, minimum 3 characters.");
            }
            else if(customer.Password!.Length < 5) {
                throw new Exception("Password too short, minimum 5 characters.");
            }
            else if(_customerRepo.GetCustomerByUsername(customer.Username!) != null) {
                throw new Exception("Username already in use.");
            }  
            return _customerRepo.AddCustomer(customer);
        }
        else{
            throw new Exception("Invalid Customer. Please check username and password");
        }
    }

    public Customer Login(LogInDTO logInDTO) {
        var customer = _mapper.Map<Customer>(logInDTO);
        if(!customer.Username.IsNullOrEmpty() && !customer.Password.IsNullOrEmpty()){
            Customer? loggedInCustomer = _customerRepo.GetCustomerByUsernamePassword(customer.Username!, customer.Password!);
            if(loggedInCustomer != null) {
                return loggedInCustomer;
            }
            throw new Exception("The username or password was incorrect");
        }
        else if (customer.Username.IsNullOrEmpty() && customer.Password.IsNullOrEmpty()){
            throw new Exception("Please enter a username and password");
        }
        else if (customer.Username.IsNullOrEmpty()){
            throw new Exception("Please enter a username");
        }
        else {
            throw new Exception("Please enter a password");
        }
    }

    
    // Update Customer password
    public Customer ChangePassword(PasswordChangeDTO passwordChangeDTO){
        
        if(passwordChangeDTO.OldPassword == passwordChangeDTO.NewPassword) {
            throw new Exception("New Password already in use");
        }
        if(!(passwordChangeDTO.Username.IsNullOrEmpty() || passwordChangeDTO.OldPassword.IsNullOrEmpty())) {
            //Find our customer by Id
            Customer? searchedCustomer = _customerRepo.GetCustomerByUsernamePassword(passwordChangeDTO.Username!, passwordChangeDTO.OldPassword!);
            if(searchedCustomer != null){
                //Update it with new values 
                if(passwordChangeDTO.NewPassword != null && passwordChangeDTO.NewPassword.Length >= 5){
                    searchedCustomer.Password = passwordChangeDTO.NewPassword;

                    //Pass it to our Customer Repository
                    _customerRepo.ChangePassword(searchedCustomer);
                    return searchedCustomer;
                }
                else {
                    throw new Exception("Password Too Short");
                }
            }
        }
        throw new Exception("Invalid Customer. Does not exist.");
    }

    // Delete Customer
    public Customer DeleteCustomer(int id){
        List<Account>? accounts = _customerRepo.GetAllAccountsById(id);
        if(accounts != null) {
            foreach(Account acc in accounts) {
                if(acc.Balance != 0) {
                    throw new Exception("Cannot delete customer when all accounts are not empty");
                }
            }
            Customer? deletedCustomer = _customerRepo.DeleteCustomer(id);
            if (deletedCustomer != null) {
                return deletedCustomer;
            }
        }
        throw new Exception("Customer to delete does not exist");
    }  
}