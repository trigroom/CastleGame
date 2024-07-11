using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddMoneyChance", order = 1)]
public class AddMoneyPerHitToProjectile : UpgradeInfo
{
    [SerializeField] private int addedMoneyChance;
    [SerializeField] private int[] characterNum;
    public override void Upgrade()
    {
        if (CharacterSelecter.instance != null)
            for (int i = 0; i < characterNum.Length; i++)
            {
                if (CharacterSelecter.instance.distributors.TryGetValue(characterNum[i], out StatsDistributor statsDistributor))
                    statsDistributor.addedMoneyChance += addedMoneyChance;
            }
    }
}
