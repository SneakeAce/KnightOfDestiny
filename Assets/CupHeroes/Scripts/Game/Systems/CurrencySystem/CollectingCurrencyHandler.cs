using UnityEngine;

public class CollectingCurrencyHandler
{
    private CurrencyCounter _currencyCounter;

    public CollectingCurrencyHandler(CurrencyCounter currencyCounter)
    {
        _currencyCounter = currencyCounter;
    }

    public void GetCurrency(int receivedCurrency)
    {
        _currencyCounter.GetCurrency(receivedCurrency);
    }
}
