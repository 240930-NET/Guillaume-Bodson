using P1.Models;
using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
using P1.API.Services;
using P1.Models.DTO;


namespace P1.API.Controllers;

[ApiController] // this Data Annonation is marking our class as a controller
[Route("api/AccountController")]
public class AccountController : Controller{

    private readonly IAccountService _accountService; // dependency injection 

    public AccountController(IAccountService accountService){
        _accountService = accountService;
    }

    [HttpGet] 
    public IActionResult GetAllAccounts(){
        try{
           return Ok(_accountService.GetAllAccounts()); // return status ok and our List of Customers
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpGet("getAccountById/{id}")]
    public IActionResult GetAccountById(int id){

        try{
           Account searchedAccount =  _accountService.GetAccountById(id);
           return Ok(searchedAccount);
        }
        catch(Exception e){
            return StatusCode(500, e.Message); 
        }
    }

    // Route to add a new customer
    [HttpPost("openAccount")]
    public IActionResult OpenAccount([FromBody] CreateAccountDTO createAccountDTO){

        try{
            Account account = _accountService.CreateAccount(createAccountDTO);
            return Ok(account);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("deposit")]
    public IActionResult Deposit([FromBody] DepositWithdrawDTO depositWithdrawDTO){

        try{
            Account account = _accountService.Deposit(depositWithdrawDTO);
            return Ok(account);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("withdraw")]
    public IActionResult Withdraw([FromBody] DepositWithdrawDTO depositWithdrawDTO){

        try{
            Account account = _accountService.Withdraw(depositWithdrawDTO);
            return Ok(account);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("transfer")]
    public IActionResult Transfer([FromBody] TransferDTO transferDTO){

        try{
            List<Account> accounts = _accountService.Transfer(transferDTO);
            return Ok(accounts);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    //Route for deleting
    [HttpDelete("closeAccount/{id}")]
    public IActionResult CloseAccount(int id){

        try{
            Account account = _accountService.DeleteAccount(id);
            return Ok(account);
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}
