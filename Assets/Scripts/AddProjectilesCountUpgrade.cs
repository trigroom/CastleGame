using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddProjectiles", order = 1)]
public class AddProjectilesCountUpgrade : UpgradeInfo
{
    [SerializeField] private int addedProjectileCount;
    [SerializeField] private int[] characterNum;
    public override void Upgrade()
    {
        if (CharacterSelecter.instance != null)
            for (int i = 0; i < characterNum.Length; i++)
            {
                if (addedProjectileCount > 0 && CharacterSelecter.instance.distributors.TryGetValue(characterNum[i], out StatsDistributor statsDistributor))
                    statsDistributor.addedProjectileCount += addedProjectileCount;
            }
    }
}
