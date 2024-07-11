using System.Collections;
using UnityEngine;

public abstract class AttackMain : MonoBehaviour
{
    [SerializeField] public int currentDamage = 0, defautDamage;
    private bool isBuffed;
    protected virtual void Start()
    {
        if (TryGetComponent(out HealthSystem healthSystem))
            if (!healthSystem.isNotNeedDistributor)
                defautDamage += healthSystem.distributor.damage;
            else if (transform.parent != null && transform.parent.TryGetComponent(out HealthSystem _healthSystem) && !healthSystem.isNotNeedDistributor)
                defautDamage += _healthSystem.distributor.damage;
        currentDamage = defautDamage;
    }
    public abstract void Attack();
    public void AttackBuff(float damageMultiplayer, float duration)
    {
        if (!isBuffed)
            StartCoroutine(RemoveBuff(damageMultiplayer, duration));
    }

    private IEnumerator RemoveBuff(float damageMultiplayer, float duration)
    {
        isBuffed = true;
        currentDamage += Mathf.CeilToInt(defautDamage * damageMultiplayer);
        yield return new WaitForSeconds(duration);
        isBuffed = false;
        currentDamage -= Mathf.CeilToInt(defautDamage * damageMultiplayer);
    }
}
