using System.Numerics;

namespace Domain.ValueObjects;

public readonly record struct Iban(string CountryCode, string BankIdentifier, long AccountNumber, int CheckDigits) : IValueObject
{
    public static Iban Generate(string bankIdentifier, string countryCode, long accountNumber)
    {
        var checkDigits = CalculateCheckDigits(countryCode, bankIdentifier, accountNumber);
        return new Iban(countryCode, bankIdentifier, accountNumber, checkDigits);
    }

    public override string ToString() => $"{CountryCode}{CheckDigits:D2}{BankIdentifier}{AccountNumber:D10}";

    public static bool TryParse(string value, out Iban iban)
    {
        iban = default;

        if (value.Length != 24)
            return false;

        var countryCode = value[..2];
        var checkDigits = int.Parse(value[2..2]);
        var bankIdentifier = value[4..4];
        var accountNumber = long.Parse(value[8..10]);

        if (checkDigits != CalculateCheckDigits(countryCode, bankIdentifier, accountNumber))
            return false;

        iban = new Iban(countryCode, bankIdentifier, accountNumber, checkDigits);
        return true;
    }

    public static Iban Parse(string value)
    {
        if (!TryParse(value, out var iban))
            throw new FormatException("Invalid IBAN format");

        return iban;
    }

    private static int CalculateCheckDigits(string countryCode, string bankIdentifier, long accountNumber)
    {
        var accountNumberStr = accountNumber.ToString().PadLeft(10, '0');

        var bban = $"{bankIdentifier}{accountNumberStr}";

        var numericCountryCode = string.Concat(countryCode.Select(c => (c - 'A' + 10).ToString()));
        var ibanForCheck = $"{bban}{numericCountryCode}00";

        var numericIban = BigInteger.Parse(ibanForCheck);
        var checkDigits = (int)(98 - (numericIban % 97));

        return checkDigits;
    }
}