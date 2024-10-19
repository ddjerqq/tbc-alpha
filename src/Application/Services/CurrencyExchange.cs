using Domain.Common;
using Domain.ValueObjects;

namespace Application.Services;

using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;

public record CurrencyResponse(string Date, string Base, Dictionary<Currency, Currency> Rates)
{
}

public class CurrencyService(HttpClient client, IMemoryCache cache) : ICurrencyExchange
{
    private readonly Uri _uri =
        new(
            $"https://api.currencyfreaks.com/v2.0/rates/latest?apikey={"JWT__KEY".FromEnvRequired()}&symbols=GEL,EUR,USD,GBP");

    private static object _key = 1;

    private async Task<CurrencyResponse> GetCurrencyExchange()
    {
        if (!cache.TryGetValue(_key, out CurrencyResponse? currencyResponse))
        {
            currencyResponse = await client.GetFromJsonAsync<CurrencyResponse>(_uri);
            cache.Set(_key, currencyResponse,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }

        return currencyResponse!;
    }

    public async Task<Money> ConvertToAsync(Money money, Currency currency)
    {
        if (money.Currency == currency) return money;
        
        var data = await GetCurrencyExchange();
        decimal rateFrom = Convert.ToDecimal(data.Rates[money.Currency]);
        decimal rateTo = Convert.ToDecimal(data.Rates[currency]);

        decimal converted = (money.Amount / rateFrom) * rateTo;
        return new Money(currency, converted);
    }
}