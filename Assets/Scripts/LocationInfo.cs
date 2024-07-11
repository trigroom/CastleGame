using UnityEngine;

[System.Serializable]
public struct LocationInfo
{
    public Transform locationPrefab;
    public float minYEnemySpawn, maxYEnemySpawn, maxCamX, minCamX, camSize,camY, castleEnemyPos, castlePlayerPos;
}
