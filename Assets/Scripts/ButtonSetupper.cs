using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetupper : MonoBehaviour
{
    [SerializeField] private GameObject upgManager;
    [SerializeField] private ButtonToSelectCharacter[] characterSelectButtons;
    [SerializeField] private ButtonToSelectHero[] heroSelectButtons;
    private void Awake()
    {
        if (!FindAnyObjectByType<UpgradesManager>())
            Instantiate(upgManager);

        for (int i = 0; i < characterSelectButtons.Length; i++)
            UpgradesManager.instance.characterSelectButtons[i] = characterSelectButtons[i];

        for (int i = 0;i < heroSelectButtons.Length;i++)
            UpgradesManager.instance.heroesSelectButtons[i] = heroSelectButtons[i];
    }

    public void StartSelectUpgManager() =>UpgradesManager.instance.StartSelect();

    public void StartGameUpgManager() => UpgradesManager.instance.StartGame();
}
