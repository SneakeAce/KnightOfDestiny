using System;
using System.Collections.Generic;

public class UpgradeCardsViewController : IDisposable
{
    private UpgradeCardsView _upgradeCardsView;

    private UpgradeSystemController _upgradeSystemController;

    public UpgradeCardsViewController(UpgradeCardsView upgradeCardsView, UpgradeSystemController upgradeSystemController)
    {
        _upgradeCardsView = upgradeCardsView;
        _upgradeSystemController = upgradeSystemController;
    }

    public void Dispose()
    {
        _upgradeSystemController.ShowCards -= OnShowCards;
        _upgradeSystemController.HideCards -= OnHideCard;
    }

    public void Initialize()
    {
        _upgradeSystemController.ShowCards += OnShowCards;
        _upgradeSystemController.HideCards += OnHideCard;
    }

    private void OnShowCards(List<IUpgradeableCard> cards)
    {
        UnityEngine.Debug.Log("OnShowCards");
        _upgradeCardsView.transform.gameObject.SetActive(true);

        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];

            if (card == null)
            {
                UnityEngine.Debug.Log($"{this.ToString()} - card is null!");
                continue;
            }

            card.Transform.gameObject.SetActive(true);
            card.Transform.SetParent(_upgradeCardsView.GetContainer(), false);
        }
    }

    private void OnHideCard(List<IUpgradeableCard> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];

            if (card == null)
            {
                UnityEngine.Debug.Log($"{this.ToString()} - card is null!");
                continue;
            }

            card.Transform.SetParent(card.Parent, false);
            card.Transform.gameObject.SetActive(false);
        }

        _upgradeCardsView.transform.gameObject.SetActive(false);
    }
}
