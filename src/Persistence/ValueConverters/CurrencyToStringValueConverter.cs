using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.ValueConverters;

public sealed class CurrencyToStringConverter() : ValueConverter<Currency, string>(
    to => to,
    from => (Currency)from);
