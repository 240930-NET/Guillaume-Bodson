using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using P1.Data.Repos;
using P1.Models;
using P1.Models.DTO;

namespace P1.API.Services;

public class AccountService : IAccountService {

    private readonly IAccountRepo _accountRepo;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepo accountRepo, IMapper mapper){
        _accountRepo =  accountRepo; 
        _mapper = mapper;
    }

    public List<Account> GetAllAccounts(){

        List<Account> result = _accountRepo.GetAllAccounts();
        return result;
        
    }

    // Get Customers by Id
    public Account GetAccountById(int id){

        Account? account = _accountRepo.GetAccountById(id);
        if(account != null){
            return account;
        }
        else{
            throw new Exception("Acccount not found");
        }
    }

    public Account CreateAccount(CreateAccountDTO createAccountDTO) {
        if (createAccountDTO.Name.IsNullOrEmpty()) {
            throw new Exception("Invalid Account Name");
        }
        var account = _mapper.Map<Account>(createAccountDTO);
        if(_accountRepo.CheckNameNotTaken(account)) {
            try {
                return _accountRepo.AddAccount(account);
            }
            catch (Exception) {
                throw new Exception("No Customer with that CustomerId");
            }
        }
        throw new Exception("Account name already in use");
    }

    public Account Deposit(DepositWithdrawDTO depositWithdrawDTO) {
        if(depositWithdrawDTO.Amount <= 0) {
            throw new Exception("Cannot deposit $0 or less");
        }
        
        //Find account by id
        Account? searchedAccount = _accountRepo.GetAccountById(depositWithdrawDTO.AccountId);
        if(searchedAccount != null){
             //Update it with new values 
            
            return _accountRepo.Deposit(depositWithdrawDTO);
        }
        throw new Exception("Invalid Account. Does not exist.");
        
    }
    public Account Withdraw(DepositWithdrawDTO depositWithdrawDTO) {
        if(depositWithdrawDTO.Amount <= 0) {
            throw new Exception("Cannot withdraw $0 or less");
        }
        
        //Find account by id
        Account? searchedAccount = _accountRepo.GetAccountById(depositWithdrawDTO.AccountId);
        if(searchedAccount != null){

            if (searchedAccount.Balance >= depositWithdrawDTO.Amount) {
                //Update it with new values 
                return _accountRepo.Withdraw(depositWithdrawDTO);
            }
            throw new Exception("Cannot withdraw more money than you have in the account");
        }
        throw new Exception("Invalid Account. Does not exist.");
    }
    public List<Account> Transfer(TransferDTO transferDTO) {
        if(transferDTO.Amount <= 0) {
            throw new Exception("Cannot transfer $0 or less");
        }
        if(transferDTO.FromAccountId == transferDTO.ToAccountId) {
            throw new Exception("Cannot transfer to the same account");
        }
        
        //Find account by id
        Account? fromAccount = _accountRepo.GetAccountById(transferDTO.FromAccountId);
        Account? toAccount = _accountRepo.GetAccountById(transferDTO.ToAccountId);
        if(fromAccount != null && toAccount != null){

            if (fromAccount.Balance >= transferDTO.Amount) {
                //Update it with new values 
            
                return _accountRepo.Transfer(transferDTO);
            }
            throw new Exception("Cannot withdraw more money than you have in the account");
        }
        else if (fromAccount == null) {
            throw new Exception("Invalid Sending Account. Does not exist.");
        }
        throw new Exception("Invalid Receiving Account. Does not exist.");  
    }

    public Account DeleteAccount(int id) {
        Account? account = _accountRepo.GetAccountById(id);
        if(account != null){
            if (account.Balance == 0) {
                account = _accountRepo.DeleteAccount(id);
                if(account != null){
                    return account;
                }
            }
            else {
                throw new Exception("Cannot delete an account which doesn't have a $0 balance");
            }
        }
        throw new Exception("This account does not exist");  
    }
}