using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.ValueConverters;

public sealed class MoneyToStringConverter() : ValueConverter<Money, string>(
    to => to.ToString(),
    from => Money.Parse(from));
