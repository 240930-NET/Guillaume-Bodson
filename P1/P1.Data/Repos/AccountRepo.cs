using P1.Models;
using P1.Models.DTO;

namespace P1.Data.Repos;

public class AccountRepo : IAccountRepo {
    
    private readonly BankContext _context; // this holds reference to the context

    public AccountRepo(BankContext context){
        _context = context;
    }

    public List<Account> GetAllAccounts(){
        return _context.Accounts.ToList();
    }

    public Account? GetAccountById(int id) {
        return _context.Accounts.Find(id);
    }

    public Account AddAccount(Account account){
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public Account? DeleteAccount(int id){
        var account = _context.Accounts.Find(id);
        if(account!=null) {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
        return account;
    }

    public bool CheckNameNotTaken(Account account) {
        List<Account> userAccounts = _context.Accounts
                                        .Where(a => a.CustomerId == account.CustomerId)
                                        .ToList();
        foreach(Account acc in userAccounts) {
            if (acc.Name == account.Name) return false;
        }
        return true;
    }

    public Account Deposit(DepositWithdrawDTO depositWithdrawDTO) {
        var account = _context.Accounts.Find(depositWithdrawDTO.AccountId);
        if(account != null) {
            account.Balance += depositWithdrawDTO.Amount;
            _context.SaveChanges();
            return account;
        }
        else throw new Exception("Account does not exist.");
    }

    public Account Withdraw(DepositWithdrawDTO depositWithdrawDTO) {
        var account = _context.Accounts.Find(depositWithdrawDTO.AccountId);
        if(account != null) {
            account.Balance -= depositWithdrawDTO.Amount;
            _context.SaveChanges();
            return account;
        }
        else throw new Exception("Account does not exist.");
    }

    public List<Account> Transfer(TransferDTO transferDTO) {
        var fromAccount = _context.Accounts.Find(transferDTO.FromAccountId);
        var toAccount = _context.Accounts.Find(transferDTO.ToAccountId);
        if(fromAccount != null && toAccount != null) {
            fromAccount.Balance -= transferDTO.Amount;
            toAccount.Balance += transferDTO.Amount;
            _context.SaveChanges();
            List<Account> accounts = [fromAccount, toAccount];
            return accounts;
        }
        else throw new Exception("Either or both of the accounts do not exist.");
    }
}