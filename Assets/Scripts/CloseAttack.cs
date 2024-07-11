using UnityEngine;
using UnityEngine.UIElements;

public class CloseAttack : AttackMain
{
    [SerializeField] private float force, duration;
    [SerializeField] private string enemyTag;

    public override void Attack() {}
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == enemyTag && collision.TryGetComponent(out HealthSystem hpSystem))
        {
            hpSystem.ChangeHealth(currentDamage, duration, force);
        }
    }
}
