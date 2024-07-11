using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddRange", order = 1)]
public class AddRange : UpgradeInfo
{
    [SerializeField] private float addedRange, addedAttackCouldown;

    [SerializeField] private int[] characterNum;
    public override void Upgrade()
    {
        if (CharacterSelecter.instance != null)
            for (int i = 0; i < characterNum.Length; i++)
            {
                if (CharacterSelecter.instance.distributors.TryGetValue(characterNum[i], out StatsDistributor statsDistributor))
                {
                    if (addedRange > 0)
                        statsDistributor.addedRange += addedRange;

                    else if (addedAttackCouldown > 0)
                        statsDistributor.addedAttackCouldown += addedAttackCouldown;

                }
            }
    }
}
