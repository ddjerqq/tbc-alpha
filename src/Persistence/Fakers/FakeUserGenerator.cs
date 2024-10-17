using Bogus;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;

namespace Persistence.Fakers;

public sealed class FakeUserGenerator : FakeEntityBuilderBase<User, Ulid>
{
    public FakeUserGenerator()
    {
        Faker
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
                switch (user.Age)
                {
                    case < 25:
                    {
                        var isStudent = f.Random.Float() < 0.4;

                        return f.Random.Double() switch
                        {
                            < .2 => new EmploymentStatus.Employed(isStudent),
                            < .6 => new EmploymentStatus.Unemployed(isStudent),
                            _ => new EmploymentStatus.SelfEmployed(isStudent),
                        };
                    }
                    case >= 70:
                        return new EmploymentStatus.Retired();
                }

                return default!;
            })
            .StrictMode(true);
    }
}

public sealed class FakeAccountGenerator : FakeEntityBuilderBase<Account, Iban>
{
    public FakeAccountGenerator(Faker<User> userFaker)
    {
        Faker
            .RuleFor(x => x.OwnerId, f => userFaker.Generate().Id)
            .RuleFor(x => x.Name, f => f.Random.Double() < 0.8 ? "Checking" : "Savings")
            .RuleFor(x => x.Balance, f => f.Finance.Amount(2000, 20000))
            .StrictMode(true);
    }
}