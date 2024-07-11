using UnityEngine;

public class DamagedDurable : MonoBehaviour
{
    [SerializeField] private float cloudSize, timeBetweenAttacks, liveTime;
    [SerializeField] private LayerMask enemyMask;
    [HideInInspector] public int changedHealth;


    private void Start()
    {
        InvokeRepeating("Attack", timeBetweenAttacks, timeBetweenAttacks);
        Destroy(gameObject, liveTime);
    }
    private void Attack()
    {
        RaycastHit2D[] damagedColliders = Physics2D.BoxCastAll(transform.position, new Vector2(cloudSize, cloudSize), 0, transform.forward, enemyMask);
        if (damagedColliders.Length > 0)
            for (int i = 0; i < damagedColliders.Length; i++)
            {
                if (damagedColliders[i].collider.gameObject.TryGetComponent<HealthSystem>(out var healthSystem))
                    healthSystem.ChangeHealth(changedHealth);
            }
    }
}
