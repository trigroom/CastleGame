using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonsManager : MonoBehaviour
{
    public GameObject[] buttons;
    [SerializeField] private Button upgradeButton;
    public TMP_Text moneyCountText;
    private void Start()
    {
        EventManager.DataUpgradeManagerLoaded += SetMoneyText;
    }
    private void SetMoneyText()
    {
        if (UpgradesManager.instance.useUpgrades != null)
        {
            bool[] usedUpg = UpgradesManager.instance.useUpgrades;
            for (int i = 1; i < usedUpg.Length; i++)
                if (usedUpg[i])
                {
                    buttons[i - 1].GetComponent<Button>().interactable = false;
                    Debug.Log(buttons[i - 1].name);
                }
            moneyCountText.text = UpgradesManager.instance.money + " $";
        }
    }

    public void UpgradeStats(int number)
    {
        UpgradesManager.instance.UpgradeInButton(number);
        moneyCountText.text = UpgradesManager.instance.money + " $";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        EventManager.DataUpgradeManagerLoaded -= SetMoneyText;
    }
}
