using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemWithDamageBoost : HealthSystem
{
    [SerializeField]private float damageMultiplayerByHealth;
    private AttackMain attackMain;

    private void Start()
    {
        attackMain = GetComponent<AttackMain>();
    }
    public override void ChangeHealth(int health)
    {
        base.ChangeHealth(health);
        attackMain.currentDamage = Mathf.CeilToInt(((1-(float)currentHealth / maxHealth) * damageMultiplayerByHealth) + attackMain.defautDamage);
    }

}
