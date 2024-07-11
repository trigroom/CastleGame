using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyAttack : AttackMain
{
    [HideInInspector]public Vector2 targetPos;
    [SerializeField] private Transform canopyProjectile;
    public override void Attack()
    {
        Instantiate(canopyProjectile, transform.position,Quaternion.identity).GetComponent<CanopyTrajectory>().SetValues(targetPos,transform.position, currentDamage);
    }
}
