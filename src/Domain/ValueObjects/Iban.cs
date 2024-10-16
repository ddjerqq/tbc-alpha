using System.Numerics;
using System.Security.Cryptography;

namespace Domain.ValueObjects;

public readonly record struct Iban(string CountryCode, string BankIdentifier, int Branch, int AccountNumber, int CheckDigits) : IValueObject
{
    public static Iban Generate(string countryCode, string bankName, int branch, int accountNumber)
    {
        var bankIdentifier = new string(bankName.Where(char.IsLetterOrDigit).ToArray()).ToUpper().PadRight(8, '0').Substring(0, 8);
        var checkDigits = CalculateCheckDigits("DE", bankIdentifier, branch, accountNumber);
        return new Iban(countryCode, bankIdentifier, branch, accountNumber, checkDigits);
    }

    public static Iban Generate(int branch, int accountNumber) => Generate(Constants.BankCountryCode, Constants.BankName, branch, accountNumber);

    public override string ToString() => $"{CountryCode}{CheckDigits:D2}{BankIdentifier}{Branch:D4}{AccountNumber:D10}";

    public static Iban Parse(string ibanString)
    {
        if (ibanString.Length != 22)
            throw new FormatException("Invalid IBAN length.");

        var countryCode = ibanString[..2];
        var checkDigits = int.Parse(ibanString[2..4]);
        var bankIdentifier = ibanString[4..12];
        var branch = int.Parse(ibanString[12..16]);
        var accountNumber = int.Parse(ibanString[16..]);

        return new Iban(countryCode, bankIdentifier, branch, accountNumber, checkDigits);
    }

    private static int CalculateCheckDigits(string countryCode, string bankIdentifier, int branch, int accountNumber)
    {
        var numericCountryCode = string.Concat(countryCode.Select(c => (c - 'A' + 10).ToString()));
        var bban = $"{bankIdentifier}{branch:D4}{accountNumber:D10}";
        var ibanForCheck = $"{bban}{numericCountryCode}00";
        return (int)(98 - BigInteger.Parse(ibanForCheck) % 97);
    }
}