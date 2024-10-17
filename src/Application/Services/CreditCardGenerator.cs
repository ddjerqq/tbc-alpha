using System.Security.Cryptography;
using Domain.ValueObjects;

namespace Application.Services;

public class CreditCardGenerator()
{
    private readonly Random _random = new();
    private readonly string VisaBin = "413220";

    private int Luhn(string cardNumber)
    {
        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int n = Convert.ToInt32(cardNumber[i].ToString());
            if (alternate)
            {
                n *= 2;
                if (n > 9) n -= 9;
            }

            sum += n;
            alternate = !alternate;
        }

        return (10 - (sum % 10)) % 10;
    }

    private string Cvc()
    {
        var cvc = "";
        for (var i = 0; i < 3; i++)
        {
            cvc += RandomNumberGenerator.GetInt32(0, 9).ToString();
        }

        return cvc;
    }

    public Card GenerateVisa()
    {
        string cardNumber = "4" + VisaBin;
        for (int i = 0; i < 8; i++)
        {
            cardNumber += RandomNumberGenerator.GetInt32(0, 9);
        }

        cardNumber += Luhn(cardNumber).ToString();
        return new Card(cardNumber, Cvc(),
            new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(_random.Next(12))
                .AddYears(5));
    }

    public Card GenerateMasterCard()
    {
        string cardNumber = _random.Next(51, 55).ToString();
        for (int i = 0; i < 13; i++)
        {
            cardNumber += RandomNumberGenerator.GetInt32(0, 9);
        }

        cardNumber += Luhn(cardNumber).ToString();
        return new Card(cardNumber, Cvc(),
            new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(_random.Next(12))
                .AddYears(5));
    }
}