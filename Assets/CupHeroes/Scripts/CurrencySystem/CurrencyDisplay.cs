using TMPro;
using UnityEngine;
using Zenject;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;

    private CurrencyController _currencyController;

    [Inject]
    private void Construct(CurrencyController currencyController)
    {
        _currencyController = currencyController;
    }

    private void Start()
    {
        _currencyController.OnCurrencyChanged += UpdateDisplay;
    }

    private void UpdateDisplay(int amountCurrency)
    {
        _amountText.text = amountCurrency.ToString();
    }

    private void OnDestroy()
    {
        _currencyController.OnCurrencyChanged -= UpdateDisplay;
    }
}
