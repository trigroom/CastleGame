using UnityEngine;

public class RobberAttack : RangeAttack
{
    public override void Attack()
    {
        int curUsedProjectile = 0;
        if (projectileInfo.Length != 1)
            curUsedProjectile = GetCurrentUsedProjectile();
        StealingMoneyProjectile projectile = Instantiate(projectileInfo[curUsedProjectile].projectilePrefab, transform.position, firePoint.rotation).GetComponent<StealingMoneyProjectile>();
        projectile.damage = currentDamage;
        projectile.moneyChance += GetComponent<HealthSystem>().distributor.addedMoneyChance;
        projectile.moneyController = FindAnyObjectByType<MoneyController>();
    }
}
