
namespace P1.Models;

public class Account
{
    public int AccountId {get; set; } //Primary Key 
    public string? Name {get; set; }
    public double Balance {get; set; } = 0;

    //Foreign Key for Customer table
    public int CustomerId {get; set;}

    //Navigation Property
    [System.Text.Json.Serialization.JsonIgnore]
    public Customer? Customer {get; set; } //One to Many relationship with Customer table

}
