using P1.Models;
using P1.Models.DTO;
namespace P1.Data.Repos;

public interface IAccountRepo{

    public List<Account> GetAllAccounts();
    public Account? GetAccountById(int id);
    public Account AddAccount(Account account);
    public Account? DeleteAccount(int id);
    public bool CheckNameNotTaken(Account account);
    public Account Deposit(DepositWithdrawDTO depositWithdrawDTO);
    public Account Withdraw(DepositWithdrawDTO depositWithdrawDTO);
    public List<Account> Transfer(TransferDTO transferDTO);
}