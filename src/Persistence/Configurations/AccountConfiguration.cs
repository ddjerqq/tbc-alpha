using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasOne(account => account.Owner)
            .WithMany(user => user.Accounts)
            .HasForeignKey(account => account.OwnerId);

        builder.HasMany(account => account.Transactions)
            .WithOne(accountTransaction => accountTransaction.Account)
            .HasForeignKey(accountTransaction => accountTransaction.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Name).HasMaxLength(32);
    }
}