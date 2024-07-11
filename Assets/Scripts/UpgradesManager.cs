using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour, IDataPersistence
{

    public int money { get; private set; }
    [SerializeField] private int maxCharactersInRound = 5;
    public bool[] useUpgrades, unlockedCharacters, unlockedHeroes, isSelectedCharacters;
    [SerializeField] public UpgradeInfo[] upgradesInfo;
    [SerializeField] public CharacterInfo[] characters, heroes;
    [SerializeField] public CharacterInfo[] selectedCharacters;
    public CharacterInfo selectedHero;
    [SerializeField] public ButtonToSelectCharacter[] characterSelectButtons;
    [SerializeField] public ButtonToSelectHero[] heroesSelectButtons;
    public int level;
    private int selectedHeroNumber;
    [SerializeField] public LocationInfo[] locationInfos;
    public static UpgradesManager instance { get; private set; }

    private void Awake()
    {
        EventManager.AllDataLoaded += StartLoadGameData;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        EventManager.MapStarted += SetCharactersStats;
        Application.targetFrameRate = 120;
        selectedCharacters = new CharacterInfo[maxCharactersInRound];
        // Invoke("SetUpgradeButtons", 0.8f);
        //Invoke("SetupUpgrades", 0.7f);
        //Invoke("ForTest", 0.5f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            DataPersistenceManager.instance.LoadGame();
        }
        if (Input.GetKey(KeyCode.I))
        {
            DataPersistenceManager.instance.SaveGame();
        }
        if (Input.GetKey(KeyCode.P))
        {
            money++;
        }
    }
    private void StartLoadGameData()
    {
        EventManager.MapEnded += DataPersistenceManager.instance.SaveGame;
        SetupUpgrades();
        SetUpgradeButtons();
        //ForTest();

        EventManager.OnDataUpgradeManagerLoaded();
    }

    public void CheckCurrentSelectedHero(int heroNumber)
    {
        if (selectedHero != null)
            heroesSelectButtons[selectedHeroNumber].DeselectHero();
        selectedHeroNumber = heroNumber;
        selectedHero = heroes[selectedHeroNumber];
    }

    private void SetupUpgrades()
    {
        for (int i = 0; i < useUpgrades.Length; i++)
        {
            if (useUpgrades[i])
                upgradesInfo[i].Upgrade();
        }

    }
    public int SelectCharacter(CharacterInfo characterInfo)
    {
        if (CheckSelectedCharactersCount() <= maxCharactersInRound)
            for (int i = 0; i < selectedCharacters.Length; i++)
                if (selectedCharacters[i] == null)
                {
                    selectedCharacters[i] = characterInfo;
                    return i;
                }
        return -1;
    }

    private bool checkUnlockedHeroes()
    {
        int unlockedHeroesCount = 0;
        for (int i = 0; i < unlockedHeroes.Length; i++)
        {
            if (unlockedHeroes[i])
            {
                heroesSelectButtons[i].SelectHero();
                unlockedHeroesCount++;
            }
        }
        if (unlockedHeroesCount > 1)
            return true;

        return false;
    }

    public void StartSelect()
    {
        bool isInstantStart = true;
        if (CheckUnlockedCharacters() <= maxCharactersInRound)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (unlockedCharacters[i])
                    characterSelectButtons[i].SelectCharacter();
            }
        }
        else
        {
            isInstantStart = false;
            int count = 0;
            for (int i = 0; i < characters.Length; i++)
            {
                if (unlockedCharacters[i])
                {
                    characterSelectButtons[i].SelectCharacter();
                    count++;
                }
                if (count == maxCharactersInRound)
                    return;
            }
        }

        if (checkUnlockedHeroes())
            isInstantStart = false;

        if (isInstantStart)
            SceneManager.LoadScene(1);
    }

    public void ChangeMoney(int amount)
    {
        money += amount;
        DataPersistenceManager.instance.SaveGame();
        Debug.Log(money + "$");
    }

    private int CheckUnlockedCharacters()
    {
        int count = 0;
        for (int i = 0; i < unlockedCharacters.Length; i++)
        {
            if (unlockedCharacters[i])
                count++;
        }
        return count;

    }

    private bool IsAllCharacterSelected()
    {
        for (int i = 0; i < unlockedCharacters.Length; i++)
            if (unlockedCharacters[i] && !selectedCharacters[i])
                return false;
        return true;
    }

    public void StartGame()
    {
        if (CheckSelectedCharactersCount() == maxCharactersInRound || IsAllCharacterSelected())
            SceneManager.LoadScene(1);
    }
    public int CheckSelectedCharactersCount()
    {
        int count = 0;
        for (int i = 0; i < selectedCharacters.Length; i++)
        {
            if (selectedCharacters[i] != null)
                count++;
        }
        return count;

    }
    public void DeselectCharacter(int numDeselectCharacter)
    {
        selectedCharacters[numDeselectCharacter] = null;
    }

    private void ForTest()
    {
        if (!useUpgrades[0])
        {
            unlockedCharacters = new bool[22];
            unlockedCharacters[0] = true;
            unlockedCharacters[1] = true;
            unlockedCharacters[2] = true;

            useUpgrades = new bool[upgradesInfo.Length];
            useUpgrades[0] = true;
            DataPersistenceManager.instance.SaveGame();
        }
    }
    private void OnDestroy()
    {
        EventManager.MapEnded -= DataPersistenceManager.instance.SaveGame;
        EventManager.MapStarted -= SetCharactersStats;
    }

    public void SaveToDataManager() => DataPersistenceManager.instance.SaveGame();
    private void SetUpgradeButtons()
    {
        if (useUpgrades != null)
        {
            UpgradeButtonsManager upgradeButtonsManager = FindAnyObjectByType<UpgradeButtonsManager>();
            for (int i = 0; i < upgradesInfo.Length; i++)
            {
                if (useUpgrades[i] && upgradesInfo[i].unlockableUpgradeButtons != null)
                    for (int j = 0; j < upgradesInfo[i].unlockableUpgradeButtons.Length; j++)
                        upgradeButtonsManager.buttons[upgradesInfo[i].unlockableUpgradeButtons[j]].SetActive(true);
            }
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public void EndGame()
    {
        Invoke("MapEndedSummon", 0.2f);
        for (int i = 0; i < selectedCharacters.Length; i++)
            if (selectedCharacters[i] != null)
                selectedCharacters[i] = null;
        DataPersistenceManager.instance.SaveGame();
    }

    private void MapEndedSummon()
    {
        SetUpgradeButtons();
        EventManager.OnMapEnded();
    }


    public void UpgradeInButton(int upgradeNumber)
    {
        if (money > upgradesInfo[upgradeNumber].cost)
        {
            useUpgrades[upgradeNumber] = true;
            money -= upgradesInfo[upgradeNumber].cost;
            DataPersistenceManager.instance.SaveGame();
            SetUpgradeButtons();
            UpgradeButtonsManager upgradeButtonsManager = FindAnyObjectByType<UpgradeButtonsManager>().GetComponent<UpgradeButtonsManager>();
            upgradeButtonsManager.buttons[upgradeNumber - 1].GetComponent<Button>().interactable = false;
            upgradeButtonsManager.moneyCountText.text = money + " $";
        }
    }
    private void SetCharactersStats()
    {
        for (int i = 0; i < upgradesInfo.Length; i++)
        {
            if (useUpgrades[i])
                upgradesInfo[i].Upgrade();
        }
        Instantiate(selectedHero.characterPrefab, new Vector2(-1, -0.25f), Quaternion.identity);
    }
    //saveAndLoad
    public void LoadData(GameData data)
    {
        this.level = data.currentLevel;

        if (useUpgrades.Length == 0)
            this.useUpgrades = new bool[data.unlockedUpgrades.Length];
        for (int i = 0; i < upgradesInfo.Length; i++)
            this.useUpgrades[i] = data.unlockedUpgrades[i];

        this.money = data.money;
    }

    public void SaveData(GameData data)
    {
        data.currentLevel = level;

        for (int i = 0; i < data.unlockedUpgrades.Length; i++)
            data.unlockedUpgrades[i] = this.useUpgrades[i];

        data.money = this.money;
    }





}
