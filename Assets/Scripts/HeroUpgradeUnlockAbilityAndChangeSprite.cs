using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "HeroUpgrades/UnlockAbilityAndChangeSprite", order = 2)]
public class HeroUpgradeUnlockAbilityAndChangeSprite : HeroUpgradeUlockAbility
{
    [SerializeField] private Sprite newSprite;
    public override void Upgrade()
    {
        base.Upgrade();
        HeroUpgradeSystem.instance.spriteToChange.sprite = newSprite;
    }
}
