using System.Collections.Generic;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour
{
    public Dictionary<int, StatsDistributor> distributors = new Dictionary<int, StatsDistributor>();
    [SerializeField] private CharacterCellInfo[] cells = new CharacterCellInfo[] { };
    [SerializeField] private MoneyController moneyController;
    [SerializeField] private Canvas hpBarsCanvas;
    [SerializeField] private Camera _camera;
    private CharacterCellInfo currentCharacterPrefab;

    public static CharacterSelecter instance { get; private set; }

    private void Start()
    {
        instance = this;
        _camera = Camera.main;
        currentCharacterPrefab = cells[0];
        Invoke("StartSetupDistributes", 0.5f);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void StartSetupDistributes()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].isActiveAndEnabled)
            {
                distributors.Add(cells[i].characterPrefab.GetComponent<HealthSystem>().characterNumber, new StatsDistributor());
            }
        }
        for (int i = 0; i < UpgradesManager.instance.useUpgrades.Length; i++)
        {
            if (UpgradesManager.instance.useUpgrades[i])
                UpgradesManager.instance.upgradesInfo[i].Upgrade();
        }
     /*   foreach (var item in distributors)
        {
            Debug.Log("distributor " + item);
        }*/
    }
    public void SpawnCharacter()
    {
        if (moneyController.money >= currentCharacterPrefab.cost)
        {
            moneyController.ChangeMoney(-currentCharacterPrefab.cost);
            Instantiate(currentCharacterPrefab.characterPrefab, new Vector3(-1.2f, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity).GetComponent<HealthSystem>().SetupHPBar(hpBarsCanvas, _camera); ;
        }
    }

    public GameObject SpawnCharacter(GameObject character, Vector2 spawnPosition)
    {
        GameObject spawnCharacter = Instantiate(character, spawnPosition, Quaternion.identity);
        spawnCharacter.GetComponent<HealthSystem>().SetupHPBar(hpBarsCanvas, _camera);
        return spawnCharacter;
    }
    public void SelectCell(CharacterCellInfo cellInfo)
    {
        currentCharacterPrefab = cellInfo;
    }
}
