using UnityEngine;

public class HealerAttack : AttackSystem
{
    [SerializeField] private int heal;
    [SerializeField] private string associateTag;
    [SerializeField] GameObject healParticles;

    private void Update()
    {
        currentAttackCouldown += Time.deltaTime;
        if (currentAttackCouldown > attackCouldown)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            if (colliders.Length > 0)
            {

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].TryGetComponent(out HealthSystem healthSystem) && colliders[i].gameObject.tag == associateTag && colliders[i].gameObject != gameObject)
                    {
                        healthSystem.ChangeHealth(-heal);
                        if (healthSystem.currentHealth < healthSystem.maxHealth)
                        {
                            GameObject particles = Instantiate(healParticles, colliders[i].transform.position, Quaternion.identity);
                            Destroy(particles, 2);
                        }
                    }
                }
                currentAttackCouldown = 0;
            }

        }

        CheckSafetyRange();
    }
}
