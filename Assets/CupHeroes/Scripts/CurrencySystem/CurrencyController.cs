using System;
using UnityEngine;

public class CurrencyController : ICurrencyController, IDisposable
{
    private IEntity _entity;

    private CurrencyCounter _currencyCounter;
    private CollectingCurrencyHandler _collectingCurrency;

    public CurrencyController(IEntity entity, CurrencyCounter currencyCounter, CollectingCurrencyHandler collectingCurrency)
    {
        _entity = entity;
        _currencyCounter = currencyCounter;
        _collectingCurrency = collectingCurrency;
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
