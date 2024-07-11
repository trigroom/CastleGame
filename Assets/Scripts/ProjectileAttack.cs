using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class ProjectileAttack : MonoBehaviour
{
    [HideInInspector] public int damage;
    [SerializeField] private string enemyTag;
    [SerializeField] private float speed, lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.Translate(Vector2.right*speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == enemyTag && collision.TryGetComponent(out HealthSystem hpSystem))
        {
            hpSystem.ChangeHealth(damage);
            HitTarget();
        }
    }

    protected virtual void HitTarget()
    {
        Destroy(gameObject);
    }
}
