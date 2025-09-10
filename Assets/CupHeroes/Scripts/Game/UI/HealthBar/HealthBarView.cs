using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : UIElement
{
    [SerializeField] private Canvas _canvas; 
    [SerializeField] private Image _filledImage;
    [SerializeField] private TextMeshProUGUI _healthAmountText;

    private Tween _currentTween;

    public void SetHealth(float receivedCurrentHealth, float receivedMaxHealth)
    {
        float currentHealth = (float)receivedCurrentHealth / receivedMaxHealth;

        _healthAmountText.text = $"{receivedCurrentHealth.ConvertTo<Int32>()} / {receivedMaxHealth.ConvertTo<Int32>()}";

        _currentTween?.Kill();

        _currentTween = _filledImage.DOFillAmount(currentHealth, 0.5f).SetEase(Ease.OutQuint);
    }

    private void OnEnable()
    {
        _canvas.worldCamera = Camera.main;
    }
}
