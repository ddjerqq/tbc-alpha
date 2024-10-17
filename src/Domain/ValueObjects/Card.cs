namespace Domain.ValueObjects;

public record Card(string number, string cvc, DateOnly validThru)
{
}