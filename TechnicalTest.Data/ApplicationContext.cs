using Microsoft.EntityFrameworkCore;
using TechnicalTest.Data.Model;

namespace TechnicalTest.Data;

public class ApplicationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=bin\\database.db;");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
    //public DbSet<Transfer> Transfers { get; set; } = null!;

    public DbSet<BankAccount> BankAccounts { get; set; } = null!;
}