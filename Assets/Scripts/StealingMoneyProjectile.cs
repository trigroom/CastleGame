using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealingMoneyProjectile : ProjectileAttack
{
    public MoneyController moneyController;
    [SerializeField] public int moneyPerHit, moneyChance;
    protected override void HitTarget()
    {
        if(Random.Range(0,100) < moneyChance)
            moneyController.ChangeMoney(moneyPerHit);
        base.HitTarget();
    }
}
