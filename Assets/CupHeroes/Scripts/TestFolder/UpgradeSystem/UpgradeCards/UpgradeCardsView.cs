using UnityEngine;

public class UpgradeCardsView : UIElement
{
    private Canvas _canvas;

    private CardsContainer _cardsContainer;

    public override void Initialize()
    {
        _canvas = GetComponent<Canvas>();

        if (_canvas == null)
            Debug.LogError($"{this.ToString()} - {nameof(_canvas)} is null!");

        _canvas.worldCamera = Camera.main;

        _cardsContainer = GetComponentInChildren<CardsContainer>();

        this.gameObject.SetActive(false);
    }

    public Transform GetContainer()
    {
        return _cardsContainer?.transform;
    }
}
