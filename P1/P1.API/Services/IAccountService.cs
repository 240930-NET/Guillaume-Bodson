using P1.Models;
using P1.Models.DTO;

namespace P1.API.Services;

public interface IAccountService {
  
    //Get all Accounts
    public List<Account> GetAllAccounts();

    // Get Account by Id
    public Account GetAccountById(int id);

    public Account CreateAccount(CreateAccountDTO createAccountDTO);

    public Account Deposit(DepositWithdrawDTO depositWithdrawDTO);
    public Account Withdraw(DepositWithdrawDTO depositWithdrawDTO);
    public List<Account> Transfer(TransferDTO transferDTO);
    // Delete Customer
    public Account DeleteAccount(int id);

}