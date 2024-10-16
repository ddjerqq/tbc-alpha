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

    public required AnnualCirculation AnnualCirculation { get; set; }
    public required ICollection<SavingGoal> SavingGoals { get; set; } = [];

    /// <summary>
    /// The amount of money the user spent this year from their credit.
    /// </summary>
    public required float CreditUtilization { get; set; }

    /// <summary>
    /// The amount of money the user spent this year.
    /// </summary>
    public required Money HistoricalSpending { get; set; }

    /// <summary>
    /// The risk tolerance of the user.
    /// </summary>
    public required Level RiskTolerance { get; set; }

    public required Currency PreferredCurrency { get; set; }
    public required ICollection<Account> Accounts { get; init; } = [];
}