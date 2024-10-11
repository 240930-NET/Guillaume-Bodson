using System.Text.Json.Serialization;

namespace P0.APP.Model {
    public class Customer {

        public string Username { get; private set; }
        public string Password { get; private set; } 

        public List<Account> Accounts { get; } = [];

        public Customer(string username, string password) {
            this.Username = username;
            this.Password = password;
        }
        
        [JsonConstructor]
        public Customer(string username, string password, List<Account> accounts) {
            this.Username = username;
            this.Password = password;
            this.Accounts = accounts;
        }
        
        public void AddAccount(Account acc) {
            this.Accounts.Add(acc);
        }

        public void RemoveAccount(Account acc) { 
            this.Accounts.Remove(acc);
        }

        public Account? GetAccountByName(string name) {
            Account? acc = this.Accounts.Find(a => String.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase));
            return acc;
        } 
    }
}