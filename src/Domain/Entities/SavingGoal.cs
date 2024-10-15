using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class SavingGoal(Ulid id) : Entity<Ulid>(id)
{
    public required string Name { get; set; }
    public required Money Value { get; set; }
    public required int Years { get; set; }
    public required Level Level { get; set; }
}