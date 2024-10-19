namespace Domain.ValueObjects;

public abstract record EmploymentStatus(bool IsStudent)
{
    public sealed record Unemployed(bool IsStudent) : EmploymentStatus(IsStudent);
    public sealed record Employed(bool IsStudent) : EmploymentStatus(IsStudent);
    public sealed record SelfEmployed(bool IsStudent) : EmploymentStatus(IsStudent);
    public sealed record Retired() : EmploymentStatus(false);

    public static implicit operator string(EmploymentStatus employmentStatus)
    {
        var status = employmentStatus switch
        {
            Unemployed => "unemployed",
            Employed => "employed",
            SelfEmployed => "self-employed",
            Retired => "retired",
            _ => throw new ArgumentException("Invalid employment status."),
        };

        return status + (employmentStatus.IsStudent ? " student" : string.Empty);
    }

    public static EmploymentStatus Parse(string value)
    {
        var isStudent = value.Contains("student");

        return value.Split(' ').First() switch
        {
            "unemployed" => new Unemployed(isStudent),
            "employed" => new Employed(isStudent),
            "self-employed" => new SelfEmployed(isStudent),
            "retired" => new Retired(),
            _ => throw new ArgumentException("Invalid employment status."),
        };
    }
}