using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyAttackSystem : AttackSystem
{
    protected override void AttackCountsCheck()
    {
        if (attacks.Length <= 1)
        {
            if (attacks[0].gameObject.TryGetComponent(out CanopyAttack canopyTrajectory))
                canopyTrajectory.targetPos = lastTarget;
            attacks[0].Attack();
        }
        else
            for (int i = 0; i < attacks.Length; i++)
            {
                if (attacks[i].gameObject.TryGetComponent(out CanopyAttack canopyTrajectory))
                    canopyTrajectory.targetPos = lastTarget;
                attacks[i].Attack();
            }
        currentAttackCouldown = 0;
    }
}
