using TMPro;
using UnityEngine;
using Zenject;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountCurrencyText;
    [SerializeField] private Canvas _hudCanvas;

    private CurrencyDisplayController _currencyDisplayController;
    private CameraController _cameraController;

    [Inject]
    private void Construct(CurrencyDisplayController currencyDisplayController, CameraController cameraController)
    {
        _currencyDisplayController = currencyDisplayController;
        _cameraController = cameraController;
    }

    public void Initialize()
    {
        _hudCanvas.worldCamera = _cameraController.Camera;

        if (_hudCanvas.worldCamera == null)
        {
            Debug.LogError("WordlCamera at HUD is null!");
            return;
        }

        _currencyDisplayController.Initialize(_amountCurrencyText);
    }
}
