using Domain.ValueObjects;

namespace Application.Services;

public interface ICurrencyExchange
{
    public Task<Money> ConvertToAsync(Money money, Currency currency);
}