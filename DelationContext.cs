namespace Delation.io.Api;

using Microsoft.EntityFrameworkCore;

public class DelationContext : DbContext
{
    private readonly string _connectionString;

    public DelationContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Delation> Delations { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString,
        ServerVersion.AutoDetect(_connectionString),
        mySqlOptions =>
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
        ));
    }
}