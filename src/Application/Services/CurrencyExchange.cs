using Domain.Common;
using Domain.ValueObjects;
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services;

public sealed record CurrencyResponse(Dictionary<Currency, Currency> Rates);

public class CurrencyService(HttpClient client, IMemoryCache cache) : ICurrencyExchange
{
    private static readonly string Uri =
        $"https://api.currencyfreaks.com/v2.0/rates/latest?" +
        $"apikey={"CURRENCY_EXCHANGE__API_KEY".FromEnvRequired()}" +
        $"&symbols=GEL,EUR,USD,GBP";

    private static readonly object Key = 1;

    private async Task<CurrencyResponse> GetCurrencyExchange()
    {
        if (!cache.TryGetValue(Key, out CurrencyResponse? currencyResponse))
        {
            currencyResponse = await client.GetFromJsonAsync<CurrencyResponse>(Uri);

            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            cache.Set(Key, currencyResponse, cacheOptions);
        }

        return currencyResponse!;
    }

    public async Task<Money> ConvertToAsync(Money money, Currency currency)
    {
        if (money.Currency == currency) return money;

        var data = await GetCurrencyExchange();
        var rateFrom = Convert.ToDecimal(data.Rates[money.Currency]);
        var rateTo = Convert.ToDecimal(data.Rates[currency]);

        var converted = (money.Amount / rateFrom) * rateTo;
        return new Money(currency, converted);
    }
}