using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopupView : UIElement
{
    [SerializeField] private Canvas _canvas;

    [SerializeField] private TextMeshProUGUI _popupText;

    [SerializeField] private float _moveUpDistance;
    [SerializeField] private float _durationMoveUp;

    private Tween _currentTween;

    public void ShowPopup(float currentValue, Transform parent)
    {
        _popupText.text = "-" + currentValue.ToString();

        _currentTween?.Kill();

        _currentTween = transform.DOMoveY(transform.position.y + _moveUpDistance, _durationMoveUp)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
        {
            ReturnToPool();

            transform.SetParent(parent);
        });
    }

    private void OnEnable()
    {
        _canvas.worldCamera = Camera.main;

        if (_popupText == null)
            _popupText = GetComponent<TextMeshProUGUI>();
    }

}
