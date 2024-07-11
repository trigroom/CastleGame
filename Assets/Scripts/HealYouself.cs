using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealYouself : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private float  healTime;
    [SerializeField] private int healAmount;
     private float currentHealTime;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        InvokeRepeating("Heal", healTime, healTime);
    }

    private void Heal() =>healthSystem.ChangeHealth(-healAmount);
}
