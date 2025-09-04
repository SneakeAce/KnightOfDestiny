using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountCurrencyText;
    [SerializeField] private Canvas _hudCanvas;

    public void Initialize()
    {
        _hudCanvas.worldCamera = Camera.main;

        if (_hudCanvas.worldCamera == null)
        {
            Debug.LogError("WordlCamera at HUD is null!");
            return;
        }
    }

    public void UpdateCurrencyDisplay(int currentAmountCurrency)
    {
        _amountCurrencyText.text = currentAmountCurrency.ToString();
    }
}
