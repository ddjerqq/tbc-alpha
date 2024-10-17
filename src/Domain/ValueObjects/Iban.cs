using System.Numerics;
using System.Security.Cryptography;

namespace Domain.ValueObjects;

public readonly record struct Iban(string CountryCode, string BankIdentifier, long AccountNumber, int CheckDigits) : IValueObject
{
    public static Iban Generate(string countryCode, string bankIdentifier, long accountNumber)
    {
        var accountNumberStr = accountNumber
            .ToString()
            .PadLeft(10, '0');

        var bankIdentifierStr = string.Concat(bankIdentifier.Select(c => (c - 'A' + 10).ToString()));
        var bban = $"{bankIdentifierStr}{accountNumberStr}";

        var numericCountryCode = string.Concat(countryCode.Select(c => (c - 'A' + 10).ToString()));
        var ibanForCheck = $"{bban}{numericCountryCode}00";

        var numericIban = BigInteger.Parse(ibanForCheck);
        var checkDigits = (int)(98 - numericIban % 97);

        return new Iban(countryCode, bankIdentifier, accountNumber, checkDigits);
    }

    public static Iban Generate(long accountNumber) => Generate(Constants.BankCountryCode, Constants.BankName, accountNumber);

    public override string ToString() => $"{CountryCode}{CheckDigits:D2}{BankIdentifier}{AccountNumber:D10}";

    public static Iban Parse(string ibanString)
    {
        if (ibanString.Length != 22)
            throw new FormatException("Invalid IBAN length.");

        var countryCode = ibanString[..2];
        var checkDigits = int.Parse(ibanString[2..4]);
        var bankIdentifier = ibanString[4..12];
        var accountNumber = int.Parse(ibanString[12..]);

        return new Iban(countryCode, bankIdentifier, accountNumber, checkDigits);
    }

    private static int CalculateCheckDigits(string countryCode, string bankIdentifier, int branch, int accountNumber)
    {
        var numericCountryCode = string.Concat(countryCode.Select(c => (c - 'A' + 10).ToString()));
        var bban = $"{bankIdentifier}{branch:D4}{accountNumber:D10}";
        var ibanForCheck = $"{bban}{numericCountryCode}00";
        return (int)(98 - BigInteger.Parse(ibanForCheck) % 97);
    }
}