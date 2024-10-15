using System.ComponentModel;
using Domain.Aggregates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

[EditorBrowsable(EditorBrowsableState.Never)]
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // builder.HasMany(user => user.Sessions)
        //     .WithOne(session => session.Performer)
        //     .HasForeignKey(session => session.PerformerId);
        //
        // builder.HasMany<Reservation>()
        //     .WithOne(reservation => reservation.ReservedBy)
        //     .HasForeignKey(reservation => reservation.ReservedById);
    }
}