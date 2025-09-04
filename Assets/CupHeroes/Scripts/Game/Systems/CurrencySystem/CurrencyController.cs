using System;
using UnityEngine;

public class CurrencyController : ICurrencyController, IDisposable
{
    private CurrencyCounter _currencyCounter;

    public CurrencyController(CurrencyCounter currencyCounter)
    {
        _currencyCounter = currencyCounter;
    }

    public event Action<int> OnCurrencyChanged;

    public void Dispose()
    {
        _currencyCounter.CurrentCurrencyAmountChanged -= GetCurrentCurrencyAmount;
    }

    public void Initialize()
    {
        _currencyCounter.CurrentCurrencyAmountChanged += GetCurrentCurrencyAmount;
    }

    private void GetCurrentCurrencyAmount(int receivedCurrency) 
    {
        OnCurrencyChanged?.Invoke(receivedCurrency);
    }
}
