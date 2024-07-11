using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private int  moneyPerTick, upgradeMPTCost, upgradeTCCost;
    [SerializeField] private float tickCouldown;
    [SerializeField] TMP_Text moneyText, moneyMinerAddPerTick, textUpgMPTCost, textUpgTCCost;
    public int moneyForGame;
    [SerializeField]private float moneyPerTickMultiplayer;
    public int money { get; private set; }
    private void Start()
    {
        StartCoroutine(MoneyMiner());
        textUpgMPTCost.text = upgradeMPTCost + "$ to " + (moneyPerTick + Mathf.CeilToInt((float)moneyPerTick / 15)) + " MPT";
        textUpgTCCost.text = upgradeTCCost + "$ to " + (tickCouldown - tickCouldown / 25)+" Tick couldown";
        ChangeMoneyMinerText();
    }
    public void ChangeMoney(int changedMoney)
    {
        if (changedMoney > 0)
            moneyForGame += changedMoney;
        money += changedMoney;
        moneyText.text = money + "$";
    }

    public void UpgradeMPT()
    {
        if (money >= upgradeMPTCost)
        {
            ChangeMoney(-upgradeMPTCost);
            upgradeMPTCost += Mathf.CeilToInt((float)upgradeMPTCost / 6);
            moneyPerTick += Mathf.CeilToInt((float)moneyPerTick / 15);
            textUpgMPTCost.text = upgradeMPTCost + "$ to " + (moneyPerTick + Mathf.CeilToInt((float)moneyPerTick / 15)) + " MPT";
            ChangeMoneyMinerText();
        }
    }

    public void UpgradeTC()
    {
        if (money >= upgradeTCCost)
        {
            ChangeMoney(-upgradeTCCost);
            upgradeTCCost += Mathf.CeilToInt((float)upgradeTCCost / 5);
            tickCouldown -= tickCouldown / 25;
            textUpgTCCost.text = upgradeTCCost + "$ to " + (tickCouldown - -tickCouldown / 25) + " Tick couldown";
            ChangeMoneyMinerText();
        }
    }

    private void ChangeMoneyMinerText()
    {
        moneyMinerAddPerTick.text = moneyPerTick + "$ / " + tickCouldown + " s";
        if (moneyPerTickMultiplayer > 0)
        {
            moneyMinerAddPerTick.text +="<b><color=#00ffffff>  x " + (moneyPerTickMultiplayer + 1)+"</color></b>";
        }
    }

    public void ChangeMoneyMultiplayer(float addedMultiplayer)
    {
        moneyPerTickMultiplayer += addedMultiplayer;
        ChangeMoneyMinerText();
    }
    private IEnumerator MoneyMiner()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickCouldown);
            ChangeMoney((int)(moneyPerTick * (moneyPerTickMultiplayer+1)));
        }
    }

}
