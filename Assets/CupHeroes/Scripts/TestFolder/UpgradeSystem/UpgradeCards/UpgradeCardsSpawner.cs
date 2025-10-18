using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeCardsSpawner
{
    private IConfigsProvider _configsProvider;
    private IUpgradeCardsFactory _upgradeCardsFactory;

    private List<RareCardType> _rarityCardTypes = new();
    private List<UpgradeType> _upgradeCardTypes = new();

    private List<CardRarityPallete> _rarityPalletes = new();
    private List<UpgradeTypePallete> _upgradePalletes = new();

    public UpgradeCardsSpawner(IConfigsProvider configsProvider, IUpgradeCardsFactory upgradeCardsFactory)
    {
        _configsProvider = configsProvider;
        _upgradeCardsFactory = upgradeCardsFactory;

        _rarityPalletes = _configsProvider.GetLibraryConfig<LibraryRarityColorConfigs>().GetConfigs<CardRarityPallete>();
        _upgradePalletes = _configsProvider.GetLibraryConfig<LibraryUpgradeColorConfigs>().GetConfigs<UpgradeTypePallete>();
    }

    public void Initialize()
    {
        GetAllEnums();
    }

    public IUpgradeableCard SpawnCard()
    {
        UpgradeCardsFactoryData data = GetData();

        IUpgradeableCard card = _upgradeCardsFactory.CreateCard(data);

        if (card == null)
        {
            UnityEngine.Debug.Log($"{this.ToString()} - currentCard is null!");
            return null;
        }

        card.Transform.SetParent(null);
        card.Transform.position = Vector3.zero;

        return card;
    }

    private void GetAllEnums()
    {
        _rarityCardTypes.Clear();
        _upgradeCardTypes.Clear();

        _rarityCardTypes = GetTypes<RareCardType>();
        _upgradeCardTypes = GetTypes<UpgradeType>();
    }

    private List<T> GetTypes<T>() where T : Enum
    {
        List<T> tempList = new List<T>();

        tempList = Enum.GetValues(typeof(T))
            .Cast<T>()
            .Where(curEnum => Convert.ToInt64(curEnum) != 0)
            .ToList();

        if (tempList.Count > 0)
            return tempList;
        else
            return new List<T>();
    }

    private UpgradeCardsFactoryData GetData()
    {
        RareCardType rareType = _rarityCardTypes[UnityEngine.Random.Range(0, _rarityCardTypes.Count)];
        UpgradeType upgradeType = _upgradeCardTypes[UnityEngine.Random.Range(0, _upgradeCardTypes.Count)];

        CardRarityPallete rarityPallete = (CardRarityPallete)GetPallete(rareType);

        if (rarityPallete == null)
            UnityEngine.Debug.Log($"{this.ToString()} - rarityPallete in GetData is null!");

        UpgradeTypePallete upgradePallete = (UpgradeTypePallete)GetPallete(upgradeType);

        if (upgradePallete == null)
            UnityEngine.Debug.Log($"{this.ToString()} - upgradePallete in GetData is null!");

        UpgradeCardsFactoryData data = new UpgradeCardsFactoryData(rarityPallete, upgradePallete);

        return data;
    }
    
    private Pallete GetPallete(Enum type)
    {
        if (type is RareCardType rareType)
        {
            for (int i = 0; i < _rarityPalletes.Count; i++)
            {
                var curRarityPallete = _rarityPalletes[i];

                if (curRarityPallete.RareType == rareType)
                    return curRarityPallete;
            }
        }
        else if (type is UpgradeType upgradeType)
        {
            for (int i = 0; i < _upgradePalletes.Count; i++)
            {
                var curUpgradePallete = _upgradePalletes[i];

                if (curUpgradePallete.UpgradeType == upgradeType)
                    return curUpgradePallete;
            }
        }

        return null;
    }
}
