using P1.Models;
using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
using P1.API.Services;
using P1.Models.DTO;


namespace P1.API.Controllers;

[ApiController] // this Data Annonation is marking our class as a controller
[Route("api/CustomerController")]
public class CustomerController : Controller{

    private readonly ICustomerService _customerService; // dependency injection 

    public CustomerController(ICustomerService customerService){
        _customerService = customerService;
    }

    [HttpGet] 
    public IActionResult GetAllCustomers(){
        try{
           return Ok(_customerService.GetAllCustomers()); // return status ok and our List of Customers
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpGet("getCustomerById/{id}")]
    public IActionResult GetCustomerById(int id){

        try{
           Customer searchedCustomer =  _customerService.GetCustomerById(id);
           return Ok(searchedCustomer);
        }
        catch(Exception e){
            return StatusCode(500, e.Message); 
        }
    }

    //Route for getting accounts
    [HttpGet("getAllAccounts/{id}")]
    public IActionResult GetAllAccountsById(int id){

        try{
            List<Account> accounts = _customerService.GetAllAccountsById(id);
            return Ok(accounts);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    // Route to add a new customer
    [HttpPost("register")]
    public IActionResult Register([FromBody] LogInDTO logInDTO){

        try{
            Customer customer = _customerService.Register(logInDTO);
            return Ok(customer);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    // Route to add a new expese
    [HttpPost("login")]
    public IActionResult LogIn([FromBody] LogInDTO logInDTO){

        try{
            Customer customer = _customerService.Login(logInDTO);
            return Ok(customer);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    //Route for editing
    [HttpPatch("changePassword")]
    public IActionResult ChangePassword([FromBody] PasswordChangeDTO passwordChangeDTO){

        try{
            Customer customer = _customerService.ChangePassword(passwordChangeDTO);
            return Ok(customer);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    //Route for deleting
    [HttpDelete("deleteCustomer/{id}")]
    public IActionResult DeleteCustomer(int id){

        try{
            Customer customer = _customerService.DeleteCustomer(id);
            return Ok(customer);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}
