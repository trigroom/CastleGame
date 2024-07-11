using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "HeroUpgrades/UnlockAbility", order = 2)]
public class HeroUpgradeUlockAbility : HeroUpgradeInfo
{
    [SerializeField] private int abilityNum;
    public override void Upgrade()
    {
        HeroUpgradeSystem.instance.attackSystem.UnlockAttack(abilityNum);
    }
}
