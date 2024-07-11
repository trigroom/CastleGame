using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSetup", menuName = "Levels/LevelSetupInfo", order = 1)]
public class LevelSetup : ScriptableObject
{
    public EnemyInfo[] enemyPrefabs_;
    public int moneyPerSecond, maxEnemiesPerSpawn, locationNumber;
    public float spawnTime, upgradeTime, moneyMultiplayer;
}
