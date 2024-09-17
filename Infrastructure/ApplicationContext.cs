using Domain.Entities;
using Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
    }
    
    public DbSet<UserProfile> UserProfile { get; set; }
    public DbSet<BankDetails> BankDetails { get; set; }
    public DbSet<TransactionInfo> TransactionInfo { get; set; }
    public DbSet<CountryLookup> CountryLookup { get; set; }
    public DbSet<UvwTransferModel> UvwTransferModel { get; set; }
    public DbSet<UvwTransactionInfo> UvwTransactionInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UvwTransferModel>().ToView("UvwTransferModel").HasNoKey();
        modelBuilder.Entity<UvwTransactionInfo>().ToView("UvwTransactionInfo").HasKey(x => x.TransactionId);
        base.OnModelCreating(modelBuilder);
    }
}