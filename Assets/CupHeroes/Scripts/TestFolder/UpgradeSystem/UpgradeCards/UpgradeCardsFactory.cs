using System.Collections.Generic;
using Zenject;

public struct UpgradeCardsFactoryData
{
    public UpgradeCardsFactoryData(CardRarityPallete cardRarityPallete, UpgradeTypePallete upgradeTypePallete)
    {
        CardRarityPallete = cardRarityPallete;
        UpgradeTypePallete = upgradeTypePallete;
    }

    public CardRarityPallete CardRarityPallete { get; }
    public UpgradeTypePallete UpgradeTypePallete { get; }
}

public class UpgradeCardsFactory : IUpgradeCardsFactory
{
    private DiContainer _container;
    private IPoolsManager _poolsManager;

    private IConfigsProvider _configsProvider;
    private List<UpgradeCardConfig> _upgradeCardConfigs;

    public UpgradeCardsFactory(DiContainer container, IPoolsManager poolsManager, IConfigsProvider configsProvider)
    {
        _container = container;
        _poolsManager = poolsManager;
        _configsProvider = configsProvider;

        _upgradeCardConfigs = _configsProvider.GetLibraryConfig<UpgradeCardLibraryConfigs>().GetConfigs<UpgradeCardConfig>();
    }

    public IUpgradeableCard CreateCard(UpgradeCardsFactoryData data)
    {
        var currentUpgradeType = data.UpgradeTypePallete.UpgradeType;

        UpgradeCardConfig currentConfig = null;

        foreach (var config in _upgradeCardConfigs)
        {
            if (config.UpgradeType == currentUpgradeType)
            {
                currentConfig = config;
                break;
            }
        }

        var pool = _poolsManager.GetPool<UpgradeType>(PoolType.UpgradeCardPool, currentUpgradeType);

        if (pool == null)
        {
            UnityEngine.Debug.Log($"{nameof(pool)} in {this.ToString()} is null!");
            return null;
        }

        UpgradeCard card = (UpgradeCard)pool.GetObjectFromPool();

        if (card == null)
        {
            UnityEngine.Debug.Log($"{this.ToString()} card is null." +
                $" Most likely, there were not enough objects in the spawn pool");

            return null;
        }

        _container.Inject(card);

        card.SetParent(card.transform.parent);
        card.SetConfig(currentConfig);
        card.SetPool(pool);

        card.SetRareType(data.CardRarityPallete.RareType);
        card.SetUpgradeType(data.UpgradeTypePallete.UpgradeType);
        card.SetMultiplierValue(GetMultiplier(card, data.CardRarityPallete.RareType));

        card.Initialize();

        card.SetComponentsColor(data.UpgradeTypePallete.Color, data.CardRarityPallete.Color);

        return card;
    }

    private float GetMultiplier(IUpgradeableCard card, RareCardType currentRareType)
    {
        float baseMultiplier = 1;

        for (int i = 0; i < card.Config.MultipliersUpgradeValueFromRarity.Count; i++)
        {
            var stats = card.Config.MultipliersUpgradeValueFromRarity[i];

            if (stats == null)
                UnityEngine.Debug.Log($"{this.ToString()} - GetMultiplier - {nameof(stats)} is null!");
                    
            if (stats.RareCardType == currentRareType)
                return stats.Multiplier;
        }

        return baseMultiplier;
    }
}
