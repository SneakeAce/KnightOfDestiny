using System;
using System.Collections.Generic;
using System.Diagnostics;

public class UpgradeSystemController : IDisposable
{
    private const int AmountCardCanBeSpawned = 3;

    private IEntityStatsManager _characterStatsManager;

    private UpgradeCardsSpawner _spawner;
    private LevelController _levelController;

    private List<IUpgradeableCard> _cards = new();

    public UpgradeSystemController(UpgradeCardsSpawner spawner, LevelController levelController)
    {
        _spawner = spawner;
        _levelController = levelController;
    }

    public event Action<List<IUpgradeableCard>> ShowCards;
    public event Action<List<IUpgradeableCard>> HideCards;
    //public event Action<IUpgradeableCard> CardSelected;

    public void Dispose()
    {
        _levelController.WaveDone -= SpawnCards;
    }

    public void Initialize()
    {
        _levelController.WaveDone += SpawnCards;

        _spawner.Initialize();
    }

    public void SetCharacterStatsManager(IEntityStatsManager manager)
    {
        _characterStatsManager = manager;
    }

    private void SpawnCards()
    {
        UnityEngine.Debug.Log($"{this.ToString()} - SpawnCards");

        List<IUpgradeableCard> tempList = new List<IUpgradeableCard>();

        for (int i = 0; i < AmountCardCanBeSpawned;)
        {
            IUpgradeableCard card = _spawner.SpawnCard();

            if (card == null)
            {
                UnityEngine.Debug.Log($"{this.ToString()} - SpawnCards - card is null!");
                continue;
            }

            var capturedCard = card;

            card.ButtonSelectCard.onClick.AddListener(() => OnCardSelected(capturedCard));

            tempList.Add(card);

            i++;
        }

        _cards.AddRange(tempList);

        ShowCards?.Invoke(_cards);
    }

    private void DespawnCards()
    {
        UnityEngine.Debug.Log($"{this.ToString()} - DespawnCards!");

        if (_cards.Count == 0)
            return;

        HideCards?.Invoke(_cards);

        foreach (var card in _cards)
        {
            UnityEngine.Debug.Log($"{this.ToString()} - DespawnCards - card = {card}");
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            IUpgradeableCard card = _cards[i];

            if (card == null)
            {
                UnityEngine.Debug.Log($"{this.ToString()} - DespawnCards - card is null!");
                continue;
            }

            card.ButtonSelectCard.onClick.RemoveAllListeners();

            card.ReturnToPool();
        }

        _cards.Clear();
    }

    private void OnCardSelected(IUpgradeableCard card)
    {
        float value = card.GetUpgradeValue();

        _characterStatsManager.ModifyStats(value, card.UpgradeType);

        UnityEngine.Debug.Log($"{this.ToString()} - OnCardSelected - card = {card} " +
            $"and upgradeType = {card.UpgradeType} " +
            $"and value = {value}");

        DespawnCards();
    }
}
