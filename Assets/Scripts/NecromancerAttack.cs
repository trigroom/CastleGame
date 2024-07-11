using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttack : AttackMain
{
    [SerializeField] private GameObject summonPrefab;
    [SerializeField] private int maxHP;

    public override void Attack()
    {
        GameObject summon = CharacterSelecter.instance.SpawnCharacter(summonPrefab, transform.position);
        summon.GetComponent<CloseAttack>().currentDamage = currentDamage;
        summon.GetComponent<HealthSystem>().maxHealth = maxHP;
        summon.GetComponent<HealthSystem>().ChangeHealth(-maxHP);
    }
}
