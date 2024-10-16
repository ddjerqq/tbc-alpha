using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.ValueConverters;

public sealed class IbanToStringValueConverter() : ValueConverter<Iban, string>(
    to => to.ToString(),
    from => Iban.Parse(from));
