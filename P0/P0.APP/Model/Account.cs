
using System.Text.Json.Serialization;

namespace P0.APP.Model {
    public class Account {

        public string Owner { get; private set; } 

        public string Name { get; set; } 

        public double Balance { get; private set; } = 0;

        public Account(string owner, string name) {
            this.Owner = owner;
            this.Name = name;
        }

        [JsonConstructor]
        public Account(string owner, string name, double balance) {
            this.Owner = owner;
            this.Name = name;
            this.Balance = balance;
        }

        public bool Deposit(double d) {
            if(d >= 0) {
                this.Balance += d;
                return true;
            }
            return false;
        }

        public bool Withdraw(double w) {
            if (w <= this.Balance && w >= 0) {
                this.Balance -= w;
                return true;
            }
            return false;
        }
    }
}