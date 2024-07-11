using System.Collections;
using UnityEngine;

public class RangeAttack : AttackMain
{
    public ProjectileInfo[] projectileInfo;

    public int projectilesCount;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] protected Transform firePoint;

    protected override void Start()
    {
        base.Start();
        StatsDistributor statsDistributor = GetComponent<HealthSystem>().distributor;
        if (statsDistributor != null)
            projectilesCount += statsDistributor.addedProjectileCount;
    }
    public override void Attack()
    {
        int curProjectile = 0;
        if (projectileInfo.Length >= 1)
            curProjectile = GetCurrentUsedProjectile();

        if (projectilesCount == 0)
        {
            SpawnProjectile(curProjectile);
        }
        else
            StartCoroutine(MultiplyAttack());
    }

    private void SpawnProjectile(int curProj)
    {
        if (curProj != -1)
        {
            if (projectileInfo[curProj].projectilePrefab.TryGetComponent(out ProjectileAttack projectileAttack))
            {
                Instantiate(projectileInfo[curProj].projectilePrefab, transform.position, firePoint.rotation);
                projectileAttack.damage = currentDamage;
            }
            else
                CharacterSelecter.instance.SpawnCharacter(projectileInfo[curProj].projectilePrefab.gameObject, firePoint.position);
        }
    }

    public int GetCurrentUsedProjectile()
    {
        int currentUsedProjectile = 0;
        int chanceToUse = Random.Range(0, 101);
        for (int i = 0; i < projectileInfo.Length; i++)
        {
            if (projectileInfo[i].chanceToUse >= chanceToUse)
            {
                currentUsedProjectile = i;
                break;
            }
            else
                currentUsedProjectile = -1;
        }
        return currentUsedProjectile;
    }
    private IEnumerator MultiplyAttack()
    {
        int curProjectile = 0;
        if (projectileInfo.Length != 1)
            curProjectile = GetCurrentUsedProjectile();
        for (int i = 0; i < projectilesCount; i++)
        {
            SpawnProjectile(curProjectile);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
