
using P0.APP.Model;

namespace P0.APP.UI {
    static class BankMenu {
        
        //Gets a user input in the form of a string
        public static int GetUserInputInt() {
            string? userInput = Console.ReadLine();


            try{
                int i = Int32.Parse(userInput!);
                return i;
            }
            catch(Exception){
                return 0;
            }
        }

        //Gets a user input in the form of a double
        public static double GetUserInputDouble() {
            string? userInput = Console.ReadLine();

            if (userInput == "q") return -2;
            try{
                double i = Double.Parse(userInput!);
                if (i < 0) return -1;
                return i;
            }
            catch(Exception){
                return -3;
            }
        }

        //Gets a user input in the form of a string
        public static string GetUserInputString() {
            string? userInput = Console.ReadLine();

            if(!String.IsNullOrWhiteSpace(userInput)) {
                return userInput;
            }
            return "";
        }

        //Greet the user
        public static void WelcomeMessage() {
            Console.WriteLine("+----------------------+");
            Console.WriteLine("| Welcome to the Bank! |");
            Console.WriteLine("+----------------------+");
        }                                                                                                                                                                            



        //Ask whether the user wants to register or login
        public static void LoginOrRegister() {
            Console.WriteLine("Please choose one of the following options");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Register");
            Console.WriteLine("9 - Quit");
        }

        //Register screen for a user to register
        public static string RegisterUsername() {
            Console.WriteLine("To register please enter a username (must be longer than 3 characters)");
            Console.WriteLine("Press 9 to go back to the main menu");
            string username = GetUserInputString();
            while(username.Length < 3) {
                if(username == "9") {
                    Console.WriteLine("Failed to register");
                    return "";
                }
                if (username.Length < 3) { 
                    Console.WriteLine("The username chosen is too short");
                    Console.WriteLine("To register please enter a username (must be longer than 3 characters)");
                    Console.WriteLine("Press 9 to go back to the main menu");
                    username = BankMenu.GetUserInputString();
                }
            }
            return username;
        }

        public static string RegisterPassword() {
            Console.WriteLine("Please enter a password for your account (must be longer than 4 characters)");
            Console.WriteLine("Press 9 to go back to the main menu");
            string password = BankMenu.GetUserInputString();
            while(password.Length < 4) {
                if(password == "9") {
                    Console.WriteLine("Failed to register");
                    return "";
                }
                Console.WriteLine("The password chosen is too short");
                Console.WriteLine("Please enter a password for your account (must be longer than 4 characters)");
                Console.WriteLine("Press 9 to go back to the main menu");
                password = BankMenu.GetUserInputString();
            }
            return password;
        }

        //Login screen for a user to log in
        public static string LoginUsername() {
            Console.WriteLine("To login please enter your username");
            Console.WriteLine("Press 9 to go back to the main menu");
            return BankMenu.GetUserInputString();
        }
        
        //Login screen for a user to put in his password
        public static string LoginPassword() {
            Console.WriteLine("To login please enter your password");
            Console.WriteLine("Press 9 to go back to the main menu");
            return BankMenu.GetUserInputString();
        }

        //Give the user options
        public static void LoggedInScreen() {
            Console.WriteLine("Please choose from one of the following options");
            Console.WriteLine("1 - Check Balance");
            Console.WriteLine("2 - Deposit");
            Console.WriteLine("3 - Withdraw");
            Console.WriteLine("4 - Transfer");
            Console.WriteLine("5 - Open new bank account");
            Console.WriteLine("6 - Close a bank account");
            Console.WriteLine("8 - Logout");
            Console.WriteLine("9 - Quit");
        }

        public static void DisplayAccounts(Customer customer, bool balance = false){
            if(customer.Accounts.Count == 0) {
                Console.WriteLine("You have no open accounts");
                return;
            }
            foreach(Account acc in customer.Accounts) {
                if(balance) Console.WriteLine($"Account name: {acc.Name}, Account balance: ${acc.Balance.ToString("0.00")}");
                else Console.WriteLine($"Account name: {acc.Name}");
            }
            Console.WriteLine();
        }

        public static Account? GetAccount(Customer customer, string question) {
            Console.WriteLine(question);
            Console.WriteLine("Press 9 to go back to the main menu");
            string accountName = GetUserInputString();
            Account? acc = customer.GetAccountByName(accountName);
            while(acc == null) {
                if(accountName == "9") return null;
                Console.WriteLine("There is no account with this name");
                Console.WriteLine(question);
                Console.WriteLine("Press 9 to go back to the main menu");
                accountName = GetUserInputString(); 
                acc = customer.GetAccountByName(accountName);
            }
            return acc;
        }

        //deposit screen
        public static double GetDepositAmount() {
            Console.WriteLine("How much money do you want to deposit into your account");
            Console.WriteLine("Enter q to quit");
            double depositAmount = GetUserInputDouble();
        
            while(depositAmount < 0) {
                if (depositAmount == -2) {
                    Console.WriteLine("Failed to deposit money");
                    return depositAmount;
                }
                Console.WriteLine("Your input was invalid please try again.");
                Console.WriteLine("How much money do you want to deposit into your account");
                Console.WriteLine("Enter q to quit");
                depositAmount = GetUserInputDouble();
            }
            return depositAmount;
        }

        //Withdraw screen / doubles as the screen to choose how much money to transfer
        public static double GetWithdrawAmount(Account acc, bool transfer = false) {
            if(!transfer) Console.WriteLine("How much money do you want to withdraw from your account?");
            else Console.WriteLine("How much money do you want to transfer?");
            Console.WriteLine("Enter q to quit");
            double withdrawAmount = GetUserInputDouble();
        
            while(withdrawAmount < 0 || withdrawAmount > acc.Balance) {
                if (withdrawAmount == -2) {
                    
                    if(!transfer) Console.WriteLine("Failed to withdraw money");
                    else Console.WriteLine("Failed to transfer money");
                    return withdrawAmount;
                }
                if(withdrawAmount > acc.Balance) {
                    if(!transfer) Console.WriteLine("You cannot withdraw more money than there is in the account");
                    else Console.WriteLine("You cannot transfer more money than there is in the account");
                    Console.WriteLine($"{acc.Name} current balance: ${acc.Balance.ToString("0.00")}");
                }
                else {
                    Console.WriteLine("Your input was invalid please try again.");
                }
                if(!transfer) Console.WriteLine("How much money do you want to withdraw from your account?");
                else Console.WriteLine("How much money do you want to transfer?");
                Console.WriteLine("Enter q to quit");
                withdrawAmount = GetUserInputDouble();
            }
            return withdrawAmount;
        }

        public static void TransferScreen() {
            throw new NotImplementedException();
        }

        public static string OpenAccountScreen() {
            Console.WriteLine("What do you want to name your account (Must be longer than 3 characters)");
            Console.WriteLine("Press 9 to go back to the main menu");
            string name = GetUserInputString();
            while(name.Length < 3) {
                if(name == "9") return name;
                Console.WriteLine("The account name given is too short");
                Console.WriteLine("What do you want to name your account (Must be longer than 3 characters)");
                Console.WriteLine("Press 9 to go back to the main menu");
                name = GetUserInputString(); 
            }
            return name;
        }

    }
}