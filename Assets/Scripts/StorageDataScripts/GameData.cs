using UnityEngine;
[System.Serializable]
public class GameData
{
    [Header("Game Progress")]
    public int money;
    public int currentLevel;
    public bool[] unlockedUpgrades;

    public GameData()
    {
        money = 0;
        currentLevel = 0;
    }
}
