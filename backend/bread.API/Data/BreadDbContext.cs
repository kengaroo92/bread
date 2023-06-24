#nullable disable

using Microsoft.EntityFrameworkCore;

public class BreadDbContext : DbContext
{
    // Entity Framework Core calls this constructor to create an instance of BreadDbContext. The 'options' parameter contains the configuration settings. Such as the Db provider 'Postgres' and connection string.
    // Configured in Program.cs class. 
    public BreadDbContext(DbContextOptions<BreadDbContext> options) : base(options)
    {
        // Stays empty because no additional initialization logic is needed at the moment.
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
}