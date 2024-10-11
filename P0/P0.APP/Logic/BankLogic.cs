using P0.APP.Model;
using P0.APP.Util;
using P0.APP.UI;
using P0.APP.Data;

namespace P0.APP.Logic {

    public static class BankLogic {

        private static List<Customer> customers = [];

        //Load data (calls the FileLogic to read the data from the file)
        public static void LoadData() {
            customers = FileLogic.DeserializeCustomers();
        }

        //Register a Customer with a username and password
        //Username must be longer than 3 characters and cannot already exist
        //Password must be longer than 4 characters
        public static Customer? Register() {
            string username = BankMenu.RegisterUsername();
            if(username == "") return null;   
            
            while(!CheckUsernameDoesntExist(username)) {
                Console.WriteLine("The username chosen already exists");
                username = BankMenu.RegisterUsername();
                if(username == "") return null;   
            }
              
            string password = BankMenu.RegisterPassword();
            if(password == "") return null;   

            Customer registeredCustomer = new(username, Security.Hash(password));
            customers.Add(registeredCustomer);

            FileLogic.SerializeAsync(customers);

            return registeredCustomer;
        }

        //Login
        public static Customer? Login() {
            
            string username = "";
            string password = "";
            Customer loggedUser =  null!;
            bool customerExists = false;
            while(!customerExists) {
                username = BankMenu.LoginUsername();
                if(username == "9") {
                    Console.WriteLine("Failed to login");
                    return null;
                }
                password = BankMenu.LoginPassword();
                if(password == "9") {
                    Console.WriteLine("Failed to login");
                    return null;
                }

                loggedUser = CheckCustomerExists(username, password)!;
                if(loggedUser == null) {
                    Console.WriteLine("An user with this username and password does not exist");
                }
                else customerExists = true;
            }
            return loggedUser;
        }

        //Deposit
        public static void Deposit(Customer customer) {
            Account? acc = BankMenu.GetAccount(customer, "Which account do you want to deposit money into?");
            if(acc == null) {
                Console.WriteLine("Failed to deposit money");
                return;
            }
            double depositAmount = BankMenu.GetDepositAmount();
            if(depositAmount == -2) {
                return;
            }
            if(!acc.Deposit(depositAmount)) {
                Console.WriteLine("Something went wrong depositing your money. Please try again");
            }
            else {
                Console.WriteLine($"{acc.Name} new balance: ${acc.Balance.ToString("0.00")}");
                FileLogic.SerializeAsync(customers);
            }
        }

        //Withdraw
        public static void Withdraw(Customer customer) {
            Account? acc = BankMenu.GetAccount(customer, "Which account do you want to withdraw money from?");
            if(acc == null) {
                Console.WriteLine("Failed to withdraw money");
                return;
            }
            double withdrawAmount = BankMenu.GetWithdrawAmount(acc);
            if(withdrawAmount == -2) {
                return;
            }
            if(!acc.Withdraw(withdrawAmount)) {
                Console.WriteLine("Something went wrong withdrawing your money. Please try again");
            }
            else {
                Console.WriteLine($"{acc.Name} new balance: ${acc.Balance.ToString("0.00")}");
                FileLogic.SerializeAsync(customers);
            }
        }

        //Transfer
        public static void Transfer(Customer customer) {
            Account? accFrom = BankMenu.GetAccount(customer, "Which account do you want to transfer money from?");
            if(accFrom == null) {
                Console.WriteLine("Failed to withdraw money");
                return;
            }
            Account? accTo = BankMenu.GetAccount(customer, "Which account do you want to transfer money to?");
            if(accTo == null) {
                Console.WriteLine("Failed to withdraw money");
                return;
            }
            if(accFrom.Equals(accTo)) {
                Console.WriteLine("Cannot transfer to the same account");
                Console.WriteLine("Failed to withdraw money");
                return;
            }
            double transferAmount = BankMenu.GetWithdrawAmount(accFrom, true);
            if(transferAmount == -2) {
                return;
            }
            if(!accFrom.Withdraw(transferAmount)) {
                Console.WriteLine("Something went wrong transfering your money. Please try again");
            }
            else {
                accTo.Deposit(transferAmount);
                Console.WriteLine($"{accFrom.Name} new balance: ${accFrom.Balance.ToString("0.00")}");
                Console.WriteLine($"{accTo.Name} new balance: ${accTo.Balance.ToString("0.00")}");
                FileLogic.SerializeAsync(customers);
            }
        }

        //Open Bank Account
        public static void OpenAccount(Customer customer) {
            string accName = BankMenu.OpenAccountScreen();

            while(customer.GetAccountByName(accName) != null) {
                Console.WriteLine("An account with that name already exists");
                accName = BankMenu.OpenAccountScreen();
            }
            if(accName == "9") {
                Console.WriteLine("Failed to open an account");
                return;
            }

            customer.AddAccount(new Account(customer.Username, accName));
            FileLogic.SerializeAsync(customers);
        }

        //Close Bank Account
        public static void CloseAccount(Customer customer) {
            Account? acc = BankMenu.GetAccount(customer, "Which account do you want to close?");
            if(acc == null) {
                Console.WriteLine("Failed to close account");
                return;
            }
            while(acc.Balance > 0) {
                Console.WriteLine("You cannot close an account whose balance is greater than 0");
                Console.WriteLine($"{acc.Name} current balance: ${acc.Balance.ToString("0.00")}");
                Console.WriteLine("Please withdraw or transfer your money before closing this account");
                acc = BankMenu.GetAccount(customer, "Which account do you want to close?");
                if(acc == null) {
                    Console.WriteLine("Failed to close account");
                    return;
                }
            }
            customer.RemoveAccount(acc);
            FileLogic.SerializeAsync(customers);
        }

        //Checks that a username isn't already taken by another customer
        private static bool CheckUsernameDoesntExist(string username) {
            return !customers.Exists(c => String.Equals(c.Username, username, StringComparison.CurrentCultureIgnoreCase));
        }

        //Checks that a customer with that username and password exists (used to login)
        private static Customer? CheckCustomerExists(string username, string password) {
            return customers.Find(c => String.Equals(c.Username, username, StringComparison.CurrentCultureIgnoreCase)
                            && String.Equals(c.Password, Security.Hash(password), StringComparison.CurrentCulture));
        }
    }
}