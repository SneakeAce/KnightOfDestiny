using System;
using TMPro;
using UnityEngine;

public class CurrencyDisplayController : IDisposable
{
    private TextMeshProUGUI _amountText;

    private ICurrencyController _currencyController;

    public CurrencyDisplayController(ICurrencyController currencyController)
    {
        _currencyController = currencyController;
    }

    public void Initialize(TextMeshProUGUI amountText)
    {
        _amountText = amountText;
        _currencyController.OnCurrencyChanged += UpdateDisplay;

        Debug.Log($"Initialize in CurrencyDisplay amounText = {amountText}");
    }

    private void UpdateDisplay(int amountCurrency)
    {
        Debug.Log($"UdpateDisplay in CurrencyDisplay amountCurrency = {amountCurrency}");

        _amountText.text = amountCurrency.ToString();
    }

    public void Dispose()
    {
        _currencyController.OnCurrencyChanged -= UpdateDisplay;
    }
}
