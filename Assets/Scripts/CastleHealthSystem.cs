using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleHealthSystem : HealthSystem
{
    [SerializeField] private GameObject endMenu;
    [SerializeField] private float endMoneyMultiplayer;
    [SerializeField] private TMP_Text moneyEndRewardText;
    protected override void Death()
    {
        endMenu.SetActive(true);
        MoneyController moneyController = FindAnyObjectByType<MoneyController>();
        if (endMoneyMultiplayer > 1)
            UpgradesManager.instance.level++;
        UpgradesManager.instance.ChangeMoney( Mathf.CeilToInt(moneyController.moneyForGame * endMoneyMultiplayer));
        UpgradesManager.instance.SaveToDataManager();
        moneyEndRewardText.text = "You reward: " +  moneyController.moneyForGame + " $";
        Time.timeScale = 0;
    }

    public void NewLevel()
    {
        Time.timeScale = 1;
        UpgradesManager.instance.EndGame();
        SceneManager.LoadScene(0);
    }
    public override void ChangeHealth(int health)
    {
        if (health > 0)
            base.ChangeHealth(health);
    }
}
