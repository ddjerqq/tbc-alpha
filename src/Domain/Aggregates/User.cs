using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Aggregates;

public sealed class User(Ulid id) : AggregateRoot<Ulid>(id)
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; set; }
    public required DateTime DateOfBirth { get; init; }

    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    public required EmploymentStatus EmploymentStatus { get; init; }

    public required ICollection<SavingGoal> SavingGoals { get; init; } = [];

    public required Currency PreferredCurrency { get; init; }

    public required ICollection<Account> Accounts { get; init; } = [];
}