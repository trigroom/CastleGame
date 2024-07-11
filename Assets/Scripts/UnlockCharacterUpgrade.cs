using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnlockCharacter", order = 1)]
public class UnlockCharacterUpgrade : UpgradeInfo
{
    [SerializeField] private int unlockedCharacterNum;
    public override void Upgrade()
    {
        if(CharacterSelecter.instance == null)
        UpgradesManager.instance.unlockedCharacters[unlockedCharacterNum] = true;
    }
}
