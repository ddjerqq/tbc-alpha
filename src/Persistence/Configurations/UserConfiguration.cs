using System.ComponentModel;
using Domain.Aggregates;
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

        builder.ComplexProperty(x => x.AnnualCirculation, annualCirculationBuilder =>
        {
            annualCirculationBuilder.Property(x => x.Income).HasColumnName("annual_income");
            annualCirculationBuilder.Property(x => x.Needs).HasColumnName("annual_needs");
            annualCirculationBuilder.Property(x => x.Wants).HasColumnName("annual_wants");
            annualCirculationBuilder.Property(x => x.Savings).HasColumnName("annual_savings");
        });

        builder.HasMany(user => user.SavingGoals)
            .WithOne(savingGoal => savingGoal.Owner)
            .HasForeignKey(savingGoal => savingGoal.OwnerId);

        builder.HasMany(user => user.Accounts)
            .WithOne(account => account.Owner)
            .HasForeignKey(account => account.OwnerId);
    }
}