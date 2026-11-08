using FraudDetection.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Infrastructure.Data;

public class FraudDbContext : DbContext
{
    public FraudDbContext(DbContextOptions<FraudDbContext> options) : base(options)
    {
    }

    // Эти свойства станут таблицами в базе данных
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Rule> Rules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}