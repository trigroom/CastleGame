using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddHealthAndDodge", order = 1)]
public class AddHealthStatsToCharacters : UpgradeInfo
{
    [SerializeField] private int addedMaxHealth, addedDodgeChance;
    [SerializeField] private float addedResist;
    [SerializeField] private int[] characterNum;

    public override void Upgrade()
    {
        if (CharacterSelecter.instance != null)
            for (int i = 0; i < characterNum.Length; i++)
            {
                if (CharacterSelecter.instance.distributors.TryGetValue(characterNum[i], out StatsDistributor statsDistributor))
                {
                    statsDistributor.addedMaxHealth += addedMaxHealth;
                    statsDistributor.addedResist += addedResist;
                    statsDistributor.addedDodgeChance += addedDodgeChance;
                }
            }
    }
}
