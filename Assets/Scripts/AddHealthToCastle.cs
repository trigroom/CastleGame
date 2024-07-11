using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddHealthToCastle", order = 1)]
public class AddHealthToCastle : UpgradeInfo
{
    public int addedMaxHp;
    public override void Upgrade()
    {
        if (CharacterSelecter.instance != null)
        {
            CastleHealthSystem castleHealthSystem = GameObject.Find("CastleTower").GetComponent<CastleHealthSystem>();
            castleHealthSystem.maxHealth += addedMaxHp;
            castleHealthSystem.SetStats();
        }
    }
}
