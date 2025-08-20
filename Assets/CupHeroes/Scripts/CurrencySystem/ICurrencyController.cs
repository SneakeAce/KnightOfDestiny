using System;

public interface ICurrencyController
{
    event Action<int> OnCurrencyChanged;
    void Initialize();
}
