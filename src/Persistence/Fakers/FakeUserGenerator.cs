using System.Transactions;
using Bogus;
using Bogus.Distributions.Gaussian;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using Transaction = Domain.Entities.Transaction;

namespace Persistence.Fakers;

public sealed class FakeUserGenerator : FakeEntityBuilderBase<User, Ulid>
{
    public FakeUserGenerator()
    {
        Faker
            .CustomInstantiator(_ => new User(Ulid.NewUlid())
            {
                FullName = default!,
                Email = default!,
                PasswordHash = default!,
                DateOfBirth = default,
                EmploymentStatus = default!,
                SavingGoals = default!,
                PreferredCurrency = default,
                Accounts = default!,
            })
            .RuleFor(x => x.Id, _ => Ulid.NewUlid())
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PasswordHash, f => BC.EnhancedHashPassword(f.Random.String2(5), 4))
            .RuleFor(x => x.DateOfBirth, f =>
            {
                int age;

                if (f.Random.Float() < .3)
                {
                    age = f.Random.Int(18, 28);
                }
                else if (f.Random.Float() < .3)
                {
                    age = f.Random.Int(29, 48);
                }
                else
                {
                    age = f.Random.Int(48, 88);
                }

                return f.Date.Between(DateTime.UtcNow.AddYears(-age), DateTime.UtcNow.AddYears(-age + 1));
            })
            .RuleFor(x => x.EmploymentStatus, (f, user) =>
            {
                if (user.Age >= 70F)
                    return new EmploymentStatus.Retired();

                var isStudent = user.Age < 25 && f.Random.Float() < 0.4;

                return f.Random.Double() switch
                {
                    < .2 => new EmploymentStatus.Employed(isStudent),
                    < .6 => new EmploymentStatus.Unemployed(isStudent),
                    _ => new EmploymentStatus.SelfEmployed(isStudent),
                };
            })
            .RuleFor(x => x.SavingGoals, (f, user) =>
            {
                List<string> types = ["car", "vacation", "education", "home", "emergency_fund"];

                var amountOfGoals = f.Random.Int(0, 5);
                return Enumerable.Range(0, amountOfGoals)
                    .Select(_ =>
                    {
                        var goalAmount = f.Random.Decimal(2000, 30000);
                        goalAmount = Math.Round(goalAmount / 1000) * 1000;

                        var amountSaved = f.Random.Decimal(0, 0.8M * goalAmount);

                        var type = f.PickRandom(types);
                        types.Remove(type);

                        return new SavingGoal(Ulid.NewUlid())
                        {
                            OwnerId = user.Id,
                            Owner = user,
                            Type = type,
                            AmountSaved = new Money((Currency)"USD", amountSaved),
                            Total = new Money((Currency)"USD", goalAmount),
                            Years = f.Random.Int(1, 5),
                        };
                    })
                    .ToList();
            })
            .RuleFor(x => x.PreferredCurrency, (Currency)"USD")
            .RuleFor(x => x.Accounts, (f, user) =>
            {
                List<string> names = ["main", "funds", "foo", "bar", "baz"];

                var amount = f.Random.Int(1, 3);

                return Enumerable.Range(0, amount)
                    .Select(_ =>
                    {
                        var iban = Iban.Generate(f.Random.Long(1000000000000000, 9999999999999999));
                        var name = f.PickRandom(names);
                        names.Remove(name);

                        return new Account(iban)
                        {
                            OwnerId = user.Id,
                            Owner = user,
                            Name = name,
                            Currency = user.PreferredCurrency,
                            Transactions = [],
                        };
                    })
                    .ToList();
            })
            .StrictMode(true);
    }

    public static void PopulateAccountTransactions(List<User> users)
    {
        var faker = new Faker();

        var accounts = users
            .SelectMany(user => user.Accounts)
            .ToList();

        foreach (var account in accounts)
        {
            var transactionCount = faker.Random.Int(0, 100);
            account.Transactions.AddRange(Enumerable.Range(0, transactionCount)
                .Select(_ =>
                {
                    var amount = faker.Random.Decimal(10, 10_000);
                    var isSender = amount < 5_000M;

                    var transaction = new Transaction(Ulid.NewUlid())
                    {
                        Amount = new Money(account.Currency, amount),
                        Date = faker.Date.Past(),
                        TransactionCategory = faker.PickRandom<TransactionCategory>(),
                        Participants = [],
                    };

                    var other = faker.PickRandom(accounts.Except([account]));
                    transaction.Participants.Add(new TransactionParticipant(transaction, other, !isSender));

                    var accountParticipation = new TransactionParticipant(transaction, account, isSender);
                    transaction.Participants.Add(accountParticipation);

                    return accountParticipation;
                }));
        }
    }
}