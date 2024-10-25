using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P1.Models;

namespace P1.Data;

public class BankContext : DbContext
{
    public DbSet<Customer> Customers {get; set; } //define DBSet of Customer
    public DbSet<Account> Accounts {get; set;} // define DBSet of Account

    public BankContext() : base(){}
    public BankContext(DbContextOptions<BankContext> options) : base(options) {}
}
