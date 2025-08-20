using System;
using UnityEngine;

public class CurrencyCounter : IDisposable
{
    private int _currentCurrencyAmount;

    public int CurrentCurrencyAmount { get; }

    public event Action<int> CurrentCurrencyAmountChanged;

    public void Dispose()
    {
        _currentCurrencyAmount = 0;
    }

    public void GetCurrency(int receivedCurrency)
    {
        Debug.Log($"GetCurrency in CurrencyCounter = {receivedCurrency}");

        _currentCurrencyAmount += receivedCurrency;

        CurrentCurrencyAmountChanged?.Invoke(_currentCurrencyAmount);
    }

    public void RemoveCurrency(int removedCurrency) 
    {
        if (removedCurrency <= 0)   
        {
            Debug.LogError($"Cannot remove negative or zero currency: {removedCurrency}");
            throw new ArgumentException("Currency amount must be positive", nameof(removedCurrency));
        }

        _currentCurrencyAmount = Mathf.Max(0, _currentCurrencyAmount - removedCurrency);

        CurrentCurrencyAmountChanged?.Invoke(_currentCurrencyAmount);
    }

}
