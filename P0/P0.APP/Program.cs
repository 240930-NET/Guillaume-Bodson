using P0.APP.Logic;
using P0.APP.Model;
using P0.APP.UI;

namespace P0.APP {
    class Program
    {
        //private static bool loggedIn = false;
        private static bool exitFlag = false;
        private static Customer? currentCustomer;
        static void Main(string[] args)
        {
            BankLogic.LoadData();
            LoggedOutMenu();
            currentCustomer = null;
        }

        public static void LoggedOutMenu() {
            BankMenu.WelcomeMessage();
            BankMenu.LoginOrRegister();

            int selectedOption = BankMenu.GetUserInputInt();
            while(selectedOption != 9 && !exitFlag) {
                switch(selectedOption) {
                    case 1:
                        currentCustomer = BankLogic.Login();
                        break;
                    case 2:
                        currentCustomer = BankLogic.Register();
                        break;
                    default:
                        Console.WriteLine($"The input was not a recognized option.");
                        break;
                }
                if (currentCustomer != null) LoggedInMenu();
                if(!exitFlag) {
                    BankMenu.LoginOrRegister();
                    selectedOption = BankMenu.GetUserInputInt();
                } 
            }
            exitFlag = true;
        }

        public static void LoggedInMenu() {
            BankMenu.LoggedInScreen();

            int selectedOption = BankMenu.GetUserInputInt();
            while(selectedOption != 9 && !exitFlag) {
                switch(selectedOption) {
                    case 1:
                        BankMenu.DisplayAccounts(currentCustomer!, true);
                        break;
                    case 2:
                        BankLogic.Deposit(currentCustomer!);
                        break;
                    case 3:
                        BankLogic.Withdraw(currentCustomer!);
                        break;
                    case 4:
                        BankLogic.Transfer(currentCustomer!);
                        break;
                    case 5:
                        BankLogic.OpenAccount(currentCustomer!);
                        break;
                    case 6:
                        BankLogic.CloseAccount(currentCustomer!);
                        break;
                    case 8:
                        currentCustomer = null;
                        LoggedOutMenu();
                        break;
                    default:
                        Console.WriteLine($"The input was not a recognized option.");
                        selectedOption = BankMenu.GetUserInputInt();
                        break;
                }
                if(!exitFlag) {
                    BankMenu.LoggedInScreen();
                    selectedOption = BankMenu.GetUserInputInt();
                } 
            }  
            exitFlag = true;
        }
    }
}