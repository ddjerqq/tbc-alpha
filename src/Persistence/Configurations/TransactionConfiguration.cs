using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasMany(transaction => transaction.Participants)
            .WithOne(participant => participant.Transaction)
            .HasForeignKey(participant => participant.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}