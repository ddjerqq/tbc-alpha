namespace Domain.ValueObjects;

public abstract record EmploymentStatus(bool IsStudent)
{
    public sealed record Unemployed(bool IsStudent) : EmploymentStatus(IsStudent);

    public sealed record Employed(bool IsStudent) : EmploymentStatus(IsStudent);

    public sealed record SelfEmployed(bool IsStudent) : EmploymentStatus(IsStudent);

    public sealed record Retired() : EmploymentStatus(false);

    public override string ToString()
    {
        var predicate = (bool isStudent) => isStudent ? "student" : "non-student";

        return this switch
        {
            Unemployed { IsStudent: var student } => $"unemployed {predicate(student)}",
            Employed { IsStudent: var student } => $"employed {predicate(student)}",
            SelfEmployed { IsStudent: var student } => $"self-employed {predicate(student)}",
            Retired => "retired",
            _ => throw new InvalidOperationException("Invalid employment status."),
        };
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