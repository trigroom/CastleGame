using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnlockHero", order = 1)]
public class UnlockHeroUpgrade : UpgradeInfo
{
    [SerializeField] private int unlockedCharacterNum;
    public override void Upgrade()
    {
        if (CharacterSelecter.instance == null)
            UpgradesManager.instance.unlockedHeroes[unlockedCharacterNum] = true;
    }
}
