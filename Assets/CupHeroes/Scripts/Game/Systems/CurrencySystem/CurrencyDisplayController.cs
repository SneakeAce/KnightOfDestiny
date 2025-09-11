using System;

public class CurrencyDisplayController : IDisposable
{
    private PlayerHUD _playerHUD;
    private ICurrencyController _currencyController;

    public CurrencyDisplayController(PlayerHUD playerHUD, ICurrencyController currencyController)
    {
        _playerHUD = playerHUD;
        _currencyController = currencyController;
    }

    public void Dispose()
    {
        _currencyController.OnCurrencyChanged -= UpdateAmountCurrency;
    }

    public void Initialize()
    {
        _currencyController.OnCurrencyChanged += UpdateAmountCurrency;
    }

    private void UpdateAmountCurrency(int amountCurrency)
    {
        _playerHUD.UpdateCurrencyDisplay(amountCurrency);
    }
}
