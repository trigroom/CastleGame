using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemWithManaBuff : HealthSystem
{
    private MoneyController moneyController;
    [SerializeField] private float addedMultiplayer;
    private void Start()
    {
        moneyController = (MoneyController)FindAnyObjectByType(typeof(MoneyController));
        moneyController.ChangeMoneyMultiplayer(addedMultiplayer);
    }

    protected override void Death()
    {
        moneyController.ChangeMoneyMultiplayer(-addedMultiplayer);
        base.Death();
    }
}
