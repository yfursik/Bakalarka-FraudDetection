using FraudDetection.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Infrastructure.Data;

// Наследуемся от DbContext - это главный класс EF Core для работы с БД
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Эти два свойства (DbSet) превратятся в таблицы "Rules" и "Transactions" в PostgreSQL
    public DbSet<Rule> Rules { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    // Здесь можно детально настраивать колонки (ограничения, индексы)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка таблицы Rules
        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(r => r.Id); // Первичный ключ
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.FieldName).IsRequired().HasMaxLength(50);
            entity.Property(r => r.Operator).IsRequired().HasMaxLength(10);
            entity.Property(r => r.Action).IsRequired().HasMaxLength(50);
        });

        // Настройка таблицы Transactions
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id); // Первичный ключ
            // Жестко задаем точность для денег (18 знаков всего, 2 после запятой)
            entity.Property(t => t.Amount).HasColumnType("decimal(18,2)"); 
            entity.Property(t => t.Currency).HasMaxLength(3);
        });
    }
}