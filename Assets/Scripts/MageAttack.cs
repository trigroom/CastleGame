using UnityEngine;

public class MageAttack : AttackMain
{
    [SerializeField] private GameObject particlesMage;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float attackRange;
    private float attackOffsetUp, attackOffsetDown;

    //доделать атаку с разных точек
    protected override void Start()
    {
        base.Start();
        AttackSystem attackSystem = GetComponent<AttackSystem>();
        attackOffsetUp = attackSystem.checkerUpOffset;
        attackOffsetDown = attackSystem.checkerDownOffset;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + attackRange, transform.position.y), Color.red, attackRange);
        Vector2 downFirePoint = new Vector2(transform.position.x, transform.position.y - attackOffsetDown);
        Debug.DrawLine(downFirePoint, new Vector2(downFirePoint.x + attackRange, downFirePoint.y), Color.red, attackRange);
        Vector2 upFirePoint = new Vector2(transform.position.x, transform.position.y + attackOffsetUp);
        Debug.DrawLine(upFirePoint, new Vector2(upFirePoint.x + attackRange, upFirePoint.y), Color.red, attackRange);
    }
    public override void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, enemyMask);

        Vector2 downFirePoint = new Vector2(transform.position.x, transform.position.y - attackOffsetDown);
        RaycastHit2D hitDown = Physics2D.Raycast(downFirePoint, Vector2.right, attackRange, enemyMask);

        Vector2 upFirePoint = new Vector2(transform.position.x, transform.position.y + attackOffsetUp);
        RaycastHit2D hitUp = Physics2D.Raycast(upFirePoint, Vector2.right, attackRange, enemyMask);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<HealthSystem>().ChangeHealth(currentDamage);
            GameObject particles = Instantiate(particlesMage, hit.transform.position, Quaternion.identity);
            Destroy(particles, 1);
        }

        else if (hitDown.collider != null)
        {
            hitDown.collider.gameObject.GetComponent<HealthSystem>().ChangeHealth(currentDamage);
            GameObject particles = Instantiate(particlesMage, hitDown.transform.position, Quaternion.identity);
            Destroy(particles, 1);
        }
        else if (hitUp.collider != null)
        {
            hitUp.collider.gameObject.GetComponent<HealthSystem>().ChangeHealth(currentDamage);
            GameObject particles = Instantiate(particlesMage, hitUp.transform.position, Quaternion.identity);
            Destroy(particles, 1);
        }
    }


}
