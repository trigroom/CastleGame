using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealthSystem : HealthSystem
{
    [SerializeField] private float spawnTime;
    [SerializeField] private HeroMovementSystem movementSystem;

    private void Start()
    {
        healthBar = FindAnyObjectByType(typeof(HeroUISetupper)).GetComponent<HeroUISetupper>().hpBarImage; 
        movementSystem = GetComponent<HeroMovementSystem>();
    }
    protected override void Death()
    {
        Invoke("SpawnHero", spawnTime);
        isDeath = true;
        movementSystem.isPlayerControl = false;
        barContainer.SetActive(false);
        gameObject.SetActive(false);
    }

    private void SpawnHero()
    {
        barContainer.SetActive(true);
        gameObject.SetActive(true);
        isDeath = false;
        currentHealth = maxHealth;
        UpdateHalthBarInfo();
        transform.position = new Vector2(-1.1f, -0.15f);
    }

    /*public override void ChangeHealth(int health)
    {
        base.ChangeHealth(health);
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }*/
}
