namespace Domain.ValueObjects;

public sealed class AnnualCirculation
{
    public required Money Income { get; set; }
    public required Money Needs { get; set; }
    public required Money Wants { get; set; }
    public required Money Savings { get; set; }
}