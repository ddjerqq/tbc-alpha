using AutoMapper.Internal;
using Bogus;
using Domain.Entities;

namespace Persistence.Fakers;

public abstract class FakeEntityBuilderBase<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct, IEquatable<TId>
{
    protected readonly Faker<TEntity> Faker = new Faker<TEntity>()
        .RuleFor(x => x.Created, f => f.Date.Between(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow.AddMonths(-2)))
        .RuleFor(x => x.CreatedBy, "test")
        .RuleFor(x => x.LastModified, (DateTime?)null)
        .RuleFor(x => x.LastModifiedBy, default(string))
        .RuleFor(x => x.Deleted, (DateTime?)null)
        .RuleFor(x => x.DeletedBy, default(string));

    public IEnumerable<TEntity> Generate(int count) => Faker.Generate(count);
}