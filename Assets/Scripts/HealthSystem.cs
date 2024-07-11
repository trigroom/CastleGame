using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public int currentDodgeChance;
    [SerializeField] public int maxHealth, dodgeChance;
    public int currentHealth { get; protected set; }
    public bool isDiscarding { get; private set; }
    private float discardingForce;
    [SerializeField] protected GameObject barContainer;
    [SerializeField] protected Image healthBar;
    [SerializeField] public float discardResist = 0;
    [SerializeField] private int expPoints;
    [SerializeField] private float healthBarOffset = 0.2f;
    public int characterNumber;
    public bool isDeath { get; protected set; }
    public StatsDistributor distributor { get; private set; }
    [SerializeField] public bool isNotNeedDistributor;

    private void Awake()
    {
        if (!isNotNeedDistributor)
            StatsDistribute();
        SetStats();
    }
    private void Update()
    {
        if (isDiscarding)
        {
            transform.position += new Vector3(discardingForce * Time.deltaTime, 0);
        }
        if (barContainer != null)
            barContainer.transform.position = new Vector2(transform.position.x, transform.position.y + healthBarOffset);
    }

    public void SetStats()
    {
        if (!isNotNeedDistributor)
            currentDodgeChance = dodgeChance;
        currentHealth = maxHealth;
    }

    private void StatsDistribute()
    {
        distributor = CharacterSelecter.instance.distributors[characterNumber];

        currentDodgeChance = dodgeChance + distributor.addedDodgeChance;
        discardResist += distributor.addedResist;
        maxHealth += distributor.addedMaxHealth;

    }
    public void ChangeHealth(int health, float discardingDuration, float _discardingForce)
    {
        ChangeHealth(health);
        if (discardResist != 1 && !isDeath)
        {
            discardingForce = _discardingForce;
            StartCoroutine(Discarding(discardingDuration - discardingDuration * discardResist));
        }
    }

    public virtual void ChangeHealth(int health)
    {
        if (currentDodgeChance < Random.Range(0, 101))
        {
            currentHealth -= health;
            if (health > 0)
                FloatingTextManager.instance.SpawnFloatingText(health.ToString(), 0.5f, 0.3f, transform.position, Color.red);
            else if(currentHealth < maxHealth && health < 0)
                FloatingTextManager.instance.SpawnFloatingText((health*-1f).ToString(), 0.5f, 0.3f, transform.position, Color.green);
            if (currentHealth <= 0)
            {
                Death();
            }
            else if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            if (currentHealth > 0)
                UpdateHalthBarInfo();
            else
                healthBar.fillAmount = 0;
        }
        else
        {
            FloatingTextManager.instance.SpawnFloatingText("dodge", 0.5f, 0.3f, transform.position, Color.white);
        }
    }

    protected void UpdateHalthBarInfo() => healthBar.fillAmount = (float)currentHealth / maxHealth;

    public void SetupHPBar(Canvas canvas, Camera _camera)
    {
        if (barContainer != null)
            barContainer.transform.SetParent(canvas.transform);
    }
    protected virtual void Death()
    {
        if (HeroUpgradeSystem.instance != null && expPoints != 0)
            HeroUpgradeSystem.instance.ChangeHeroExp(expPoints);
        isDeath = true;
        Destroy(barContainer.gameObject);
        Destroy(gameObject);
    }

    private IEnumerator Discarding(float duration)
    {
        isDiscarding = true;
        yield return new WaitForSeconds(duration);
        isDiscarding = false;
    }
}
