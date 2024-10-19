using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class SavingGoal(Ulid id) : Entity<Ulid>(id)
{
    public required Ulid OwnerId { get; set; }
    public required User Owner { get; set; }

    public required string Name { get; set; }

    public required Money AmountSaved { get; set; }
    public required Money Total { get; set; }
    public required int Years { get; set; }
    public required Level Level { get; set; }
}