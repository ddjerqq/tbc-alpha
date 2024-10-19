using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class SavingGoalConfiguration : IEntityTypeConfiguration<SavingGoal>
{
    public void Configure(EntityTypeBuilder<SavingGoal> builder)
    {
        builder.HasOne(savingGoal => savingGoal.Owner)
            .WithMany(user => user.SavingGoals)
            .HasForeignKey(savingGoal => savingGoal.OwnerId);

        builder.Property(x => x.Type).HasMaxLength(32);
    }
}