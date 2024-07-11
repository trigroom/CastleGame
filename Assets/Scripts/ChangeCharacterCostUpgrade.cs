using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DecreaseCost", order = 1)]
public class ChangeCharacterCostUpgrade : UpgradeInfo
{
    [SerializeField] private int changedCost;
    [SerializeField] private int[] characterNum;

    public override void Upgrade()
    {
        if(CharacterSelecter.instance != null)
        for (int i = 0; i < characterNum.Length; i++)
        {
            if (CharacterSelecter.instance.distributors.TryGetValue(characterNum[i], out StatsDistributor statsDistributor))
                statsDistributor.removedCost += changedCost;
        }
    }
}
