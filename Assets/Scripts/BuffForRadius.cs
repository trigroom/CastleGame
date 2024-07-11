using UnityEngine;

public class BuffForRadius : MonoBehaviour
{
    [SerializeField] private BuffInfo buff;
    [SerializeField]private float radius, attackCouldown;
    [SerializeField] private GameObject effectParticles;
    [SerializeField] private LayerMask buffedCharactersMask;
    private float currentAttackCouldown;

    private void Update()
    {
        currentAttackCouldown += Time.deltaTime;
        if (currentAttackCouldown > attackCouldown)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, buffedCharactersMask);

            buff.TakeBuff(colliders);
            currentAttackCouldown = 0;
        }
    }
}
