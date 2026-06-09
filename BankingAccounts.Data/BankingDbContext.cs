using BankingAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingAccounts.Data;

public sealed class BankingDbContext(DbContextOptions<BankingDbContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(account => account.Id);
            entity.HasIndex(account => account.AccountNumber).IsUnique();
            entity.Property(account => account.AccountNumber).HasMaxLength(30).IsRequired();
            entity.Property(account => account.HolderName).HasMaxLength(120).IsRequired();
            entity.Property(account => account.Currency).HasMaxLength(3).IsRequired();
            entity.Property(account => account.Balance).HasPrecision(18, 2);
        });
    }
}
