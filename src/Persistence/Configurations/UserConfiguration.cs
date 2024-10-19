using System.ComponentModel;
using Domain.Aggregates;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

[EditorBrowsable(EditorBrowsableState.Never)]
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FullName).HasMaxLength(32);
        builder.Property(x => x.Email).HasMaxLength(64);
        builder.Property(x => x.PasswordHash).HasMaxLength(64);

        builder.Property(x => x.EmploymentStatus)
            .HasConversion<string>(
                to => to,
                from => EmploymentStatus.Parse(from));

        builder.HasMany(user => user.SavingGoals)
            .WithOne(savingGoal => savingGoal.Owner)
            .HasForeignKey(savingGoal => savingGoal.OwnerId);

        builder.HasMany(user => user.Accounts)
            .WithOne(account => account.Owner)
            .HasForeignKey(account => account.OwnerId);
    }
}