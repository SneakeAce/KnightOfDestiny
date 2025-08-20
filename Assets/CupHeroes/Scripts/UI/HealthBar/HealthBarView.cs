using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image _filledImage;
    [SerializeField] private TextMeshProUGUI _healthAmountText;

    public void SetHealth(float receivedCurrentHealth, float receivedMaxHealth)
    {
        _filledImage.fillAmount = (float)receivedCurrentHealth / receivedMaxHealth;

        _healthAmountText.text = $"{receivedCurrentHealth.ConvertTo<Int32>()} / {receivedMaxHealth.ConvertTo<Int32>()}";
    }
}
