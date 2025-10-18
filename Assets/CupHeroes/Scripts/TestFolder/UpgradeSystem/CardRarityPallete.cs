using System;
using UnityEngine;

[Serializable]
public class CardRarityPallete : Pallete
{
    [field: SerializeField] public RareCardType RareType { get; private set; }
}
