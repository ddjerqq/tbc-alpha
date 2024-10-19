using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class TransactionParticipantConfiguration : IEntityTypeConfiguration<TransactionParticipant>
{
    public void Configure(EntityTypeBuilder<TransactionParticipant> builder)
    {
        builder.HasQueryFilter(transactionParticipant =>
            transactionParticipant.Transaction.Deleted == null && transactionParticipant.Account.Deleted == null);

        builder.HasKey(cs => new { cs.TransactionId, cs.AccountId });

        builder.HasOne(transactionParticipant => transactionParticipant.Transaction)
            .WithMany(transaction => transaction.Participants)
            .HasForeignKey(x => x.TransactionId);

        builder.HasOne(transactionParticipant => transactionParticipant.Account)
            .WithMany(account => account.Transactions)
            .HasForeignKey(transactionParticipant => transactionParticipant.AccountId);
    }
}