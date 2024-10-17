using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.ValueConverters;

public sealed class EmploymentStatusToStringValueConverter()
    : ValueConverter<EmploymentStatus, string>(
        to => to.ToString(),
        from => EmploymentStatus.Parse(from));