using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Aggregates;

public sealed class User : AggregateRoot<Ulid>
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; private set; }
    public Currency PreferredCurrency { get; set; }
    public ICollection<Account> Accounts { get; init; }

    // for ef core
    private User(Ulid id, string fullName, string email, Currency preferredCurrency, string passwordHash, ICollection<Account> accounts) : base(id)
    {
        FullName = fullName;
        Email = email;
        PreferredCurrency = preferredCurrency;
        PasswordHash = passwordHash;
        Accounts = accounts;
    }

    public User(string fullName, string email, Currency preferredCurrency, string password) : this(
        Ulid.NewUlid(),
        fullName,
        email,
        preferredCurrency,
        BC.EnhancedHashPassword(password),
        [])
    {
    }

    public bool TryChangePassword(string currentPassword, string newPassword)
    {
        if (!BC.EnhancedVerify(currentPassword, PasswordHash))
            return false;

        PasswordHash = BC.EnhancedHashPassword(newPassword);
        return true;
    }
}