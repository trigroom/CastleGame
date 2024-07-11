using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public float attackRange, safetyRange, attackCouldown;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected AttackMain[] attacks;
    protected MovementSystem movementSystem;
    protected float currentAttackCouldown;
    [SerializeField] public float checkerUpOffset, checkerDownOffset;
    public bool isDrawDebugLine;
    [SerializeField]protected Vector2 lastTarget;
    private Vector3 hitDir = Vector3.right;
    private void Start()
    {
      /*  if(attackRange < 0)
            hitDir = -Vector3.right;*/
        StatsDistributor statsDistributor = GetComponent<HealthSystem>().distributor;
        if (statsDistributor != null)
        {
            attackRange += statsDistributor.addedRange;
            safetyRange += statsDistributor.addedRange;
            attackCouldown -= statsDistributor.addedAttackCouldown;
        }
        movementSystem = GetComponent<MovementSystem>();
    }
    protected virtual void Update()
    {
        CheckAttackRange();
        CheckSafetyRange();
        if (isDrawDebugLine)
        {
            Debug.DrawRay(transform.position, hitDir, Color.red, attackRange);
            
            Vector2 upPosition = new Vector2(transform.position.x, transform.position.y + checkerUpOffset);
            Debug.DrawRay(upPosition, hitDir, Color.blue, attackRange);

            Vector2 downPosition = new Vector2(transform.position.x, transform.position.y - checkerDownOffset);
            Debug.DrawRay(downPosition, hitDir, Color.blue, attackRange);
        }
    }
    protected virtual void CheckAttackRange()
    {
        if (attackCouldown <= currentAttackCouldown)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, hitDir, attackRange, enemyMask);
            
            Vector2 upPosition = new Vector2(transform.position.x, transform.position.y + checkerUpOffset);
            
            RaycastHit2D hitUp = Physics2D.Raycast(upPosition, hitDir, attackRange, enemyMask);
            
            Vector2 downPosition = new Vector2(transform.position.x, transform.position.y - checkerDownOffset);
            RaycastHit2D hitDown = Physics2D.Raycast(downPosition, hitDir, attackRange, enemyMask);

            if (hit.collider != null)
            {
                lastTarget = hit.collider.transform.position;
                CalculateAngle(lastTarget);
                AttackCountsCheck();
                return;
            }
            if (hitUp.collider != null)
            {
                lastTarget = hitUp.collider.transform.position;
                CalculateAngle(lastTarget);
                AttackCountsCheck();
                return;
            }
            if (hitDown.collider != null)
            {
                lastTarget = hitDown.collider.transform.position;
                CalculateAngle(lastTarget);
                AttackCountsCheck();
                return;
            }
        }
        // все враги не двигают свой рейкаст хит
        currentAttackCouldown += Time.deltaTime;

    }

    protected virtual void AttackCountsCheck()
    {
        if (attacks.Length <= 1)
            attacks[0].Attack();
        else
            for (int i = 0; i < attacks.Length; i++)
            {
                attacks[i].Attack();
            }
        currentAttackCouldown = 0;
    }

    private void CalculateAngle(Vector2 targetPosition)
    {
        Vector2 attackDirection = targetPosition - (Vector2)transform.position;
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, attackAngle);
    }
    protected void CheckSafetyRange()
    {
        if (safetyRange != 0)
        {
            Vector2 upPosition = new Vector2(transform.position.x, transform.position.y + checkerUpOffset);
            RaycastHit2D hitSecondUp = Physics2D.Raycast(upPosition, hitDir, safetyRange, enemyMask);
            Vector2 downPosition = new Vector2(transform.position.x, transform.position.y - checkerDownOffset);
            RaycastHit2D hitSecondDown = Physics2D.Raycast(downPosition, hitDir, safetyRange, enemyMask);
            RaycastHit2D hitSecond = Physics2D.Raycast(transform.position, hitDir, safetyRange, enemyMask);
            
            // Debug.Log((hitSecond.collider != null) +" "+ (hitSecondUp.collider != null) + " " + (hitSecondDown.collider != null) + " " + (movementSystem.currentMoveSpeed != 0));
            if (hitSecond.collider != null || hitSecondUp.collider != null || hitSecondDown.collider != null && movementSystem.currentMoveSpeed != 0)
            {
                movementSystem.StopMovement();
            }
            else if (hitSecond.collider == null && hitSecondUp.collider == null && hitSecondDown.collider == null && movementSystem.currentMoveSpeed == 0)
            {
                movementSystem.ChangeMoveSpeed();
            }
        }

    }
}
