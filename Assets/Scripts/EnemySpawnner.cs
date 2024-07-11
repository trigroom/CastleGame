using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    private int currentMoney;

    [SerializeField] private float xSpawn, ySpawnMin, ySpawnMax, spawnTime, upgradeTime, moneyMultiplayer;
    [SerializeField] private int moneyPerSecond, maxEnemiesPerSpawn;
    [SerializeField] private EnemyInfo[] enemyPrefabs;
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas hpBarsCanvas;
    [SerializeField] private LevelSetup[] levelSetups;
    [SerializeField] private TMP_Text levelText;
    private int level;

    private void Start()
    {
        SetupLevelContent();
        InvokeRepeating("UpgradeStats", upgradeTime, upgradeTime);
        InvokeRepeating("AddMoney", spawnTime, spawnTime);
        EventManager.OnMapStarted();
    }

    private void SetupLevelContent()
    {
        level = UpgradesManager.instance.level;
        LevelSetup curSetup;
        if (level >= levelSetups.Length - 1)
            curSetup = levelSetups[levelSetups.Length - 1];
        else
            curSetup = levelSetups[level];

        LocationInfo locInfo = UpgradesManager.instance.locationInfos[curSetup.locationNumber];

        _camera.orthographicSize = locInfo.camSize;
        GameObject.Find("CastleTower").transform.position += new Vector3(locInfo.castlePlayerPos, 0, 0);
        GameObject.Find("CastleTowerEnemy").transform.position += new Vector3(locInfo.castleEnemyPos, 0, 0);

        CameraMovement cameraMovement = _camera.gameObject.GetComponent<CameraMovement>();
        cameraMovement.transform.position = new Vector3(cameraMovement.transform.position.x, locInfo.camY, -10);
        cameraMovement.maxX = locInfo.maxCamX;
        cameraMovement.minX = locInfo.minCamX;
        Instantiate(locInfo.locationPrefab, Vector2.zero, Quaternion.identity);

        xSpawn = locInfo.castleEnemyPos;
        ySpawnMin = locInfo.minYEnemySpawn;
        ySpawnMax = locInfo.maxYEnemySpawn;

        spawnTime = curSetup.spawnTime;
        upgradeTime = curSetup.upgradeTime;
        maxEnemiesPerSpawn = curSetup.maxEnemiesPerSpawn;
        enemyPrefabs = curSetup.enemyPrefabs_;
        moneyPerSecond = curSetup.moneyPerSecond;
        moneyMultiplayer = curSetup.moneyMultiplayer;
        levelText.text = "- " + level + " level -";
        Destroy(levelText.gameObject, 2);
    }

    private void AddMoney()
    {
        currentMoney += moneyPerSecond;
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        for (int i = 0; i < maxEnemiesPerSpawn; i++)
        {
            int curEnemy = Random.Range(0, enemyPrefabs.Length);
            if (enemyPrefabs[curEnemy].enemyCost < currentMoney)
            {
                yield return new WaitForSeconds(spawnTime / maxEnemiesPerSpawn);
                currentMoney -= enemyPrefabs[curEnemy].enemyCost;
                Instantiate(enemyPrefabs[curEnemy].enemyPref, new Vector2(xSpawn, Random.Range(ySpawnMin, ySpawnMax)), Quaternion.identity).GetComponent<HealthSystem>().SetupHPBar(hpBarsCanvas, _camera);
            }
        }
    }

    private void UpgradeStats()
    {
        moneyPerSecond += Mathf.CeilToInt((float)moneyPerSecond * moneyMultiplayer);
        maxEnemiesPerSpawn++;
    }

}
