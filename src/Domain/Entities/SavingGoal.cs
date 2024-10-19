using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class SavingGoal(Ulid id) : Entity<Ulid>(id)
{
    public required Ulid OwnerId { get; init; }
    public required User Owner { get; init; }

    public required string Name { get; init; }

    public required Money AmountSaved { get; set; }
    public float PercentageSaved => (float)AmountSaved.Amount / (float)Total.Amount * 100;
    public required Money Total { get; init; }

    public required int Years { get; init; }
}