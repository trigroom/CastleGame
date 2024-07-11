using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroAttackSystem : AttackSystem
{
    private HeroMovementSystem heroMovementSystem;
    private HeroHealthSystem heroHealthSystem;
    private float secondCurrentAttackCouldown, thirdCurrentAttackCouldown;
    [SerializeField] private float secondAttackCouldown, thirdAttackCouldown;
    [SerializeField] private bool[] isUnlockedAttacks;
     private Image[] reloadAttackImage;
    [SerializeField] private GameObject[] abilityContainers;


    private void Awake()
    {
        HeroUISetupper selectedCharacterSetupper = FindAnyObjectByType(typeof(HeroUISetupper)).GetComponent<HeroUISetupper>();
        abilityContainers = new GameObject[3];
        isUnlockedAttacks = new bool[3];
        isUnlockedAttacks[0] = true;
        reloadAttackImage = new Image[3];
        for (int i = 0; i < selectedCharacterSetupper.reloadImages.Length; i++)
        {
            reloadAttackImage[i] = selectedCharacterSetupper.reloadImages[i];
            abilityContainers[i] = selectedCharacterSetupper.abilityContainers[i];
        }
        heroMovementSystem = GetComponent<HeroMovementSystem>();
        heroHealthSystem = GetComponent<HeroHealthSystem>();
    }
    protected override void Update()
    {
        if (!heroHealthSystem.isDeath)
        {
            if (!heroMovementSystem.isPlayerControl)
                base.Update();
            else
            {
                if (attackCouldown <= currentAttackCouldown && Input.GetKeyDown(KeyCode.Space))
                {
                    attacks[0].Attack();
                    currentAttackCouldown = 0;
                }

                else if (isUnlockedAttacks[0] && secondAttackCouldown <= secondCurrentAttackCouldown && Input.GetKeyDown(KeyCode.Z))
                {
                    attacks[1].Attack();
                    secondCurrentAttackCouldown = 0;
                }

                else if (isUnlockedAttacks[1] && thirdAttackCouldown <= thirdCurrentAttackCouldown && Input.GetKeyDown(KeyCode.X))
                {
                    attacks[2].Attack();
                    thirdCurrentAttackCouldown = 0;
                }

            }
            currentAttackCouldown += Time.deltaTime;
            reloadAttackImage[0].fillAmount = (attackCouldown - currentAttackCouldown) / attackCouldown;
            if (isUnlockedAttacks[0])
            {
                secondCurrentAttackCouldown += Time.deltaTime;
                reloadAttackImage[1].fillAmount = (secondAttackCouldown - secondCurrentAttackCouldown) / secondAttackCouldown;
            }
            if (isUnlockedAttacks[1])
            {
                thirdCurrentAttackCouldown += Time.deltaTime;
                reloadAttackImage[2].fillAmount = (thirdAttackCouldown - thirdCurrentAttackCouldown) / thirdAttackCouldown;
            }
        }
    }

    public void UnlockAttack(int attackNumber)
    {
        abilityContainers[attackNumber].SetActive(true);
        isUnlockedAttacks[attackNumber] = true;
    }

    protected override void CheckAttackRange()
    {
        if (attackCouldown <= currentAttackCouldown)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.transform.right, attackRange, enemyMask);
            if (hit.collider != null)
            {
                attacks[0].Attack();
                currentAttackCouldown = 0;
            }
        }
        if (isUnlockedAttacks[1] && secondAttackCouldown <= secondCurrentAttackCouldown)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.transform.right, attackRange, enemyMask);
            if (hit.collider != null)
            {
                attacks[1].Attack();
                secondCurrentAttackCouldown = 0;
            }
        }
        if (isUnlockedAttacks[2] && thirdAttackCouldown <= thirdCurrentAttackCouldown)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.transform.right, attackRange, enemyMask);
            if (hit.collider != null)
            {
                attacks[2].Attack();
                thirdCurrentAttackCouldown = 0;
            }
        }
    }
}
