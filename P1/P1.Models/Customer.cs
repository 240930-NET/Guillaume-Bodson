namespace P1.Models;

public class Customer
{
    public int CustomerId {get; set; } //primary key
    public string? Username {get; set; }
    public string? Password {get; set; } 

    public List<Account> Accounts {get; set;} = []; //Collection of Accounts related to this customer

    public Customer(){}

    public Customer(string username, string password) {
        this.Username = username;
        this.Password = password;
    }
}
