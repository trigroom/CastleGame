using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroUpgradeSystem : MonoBehaviour
{
    private int expLevel;
    private int curExp;
    [SerializeField] private int[] expToNextLevel;
    [SerializeField] private HeroUpgradeInfo[] heroUpgradeInfos;
    [SerializeField] private Sprite[] rankSprites;
    private Image expBar, rankImage;
    public HeroAttackSystem attackSystem;
    public SpriteRenderer spriteToChange;

    public static HeroUpgradeSystem instance { get; private set; }

    private void Awake()
    {
        instance = this;
        HeroUISetupper heroSetupper = FindAnyObjectByType(typeof(HeroUISetupper)).GetComponent<HeroUISetupper>();
        expBar = heroSetupper.expBarImage;
        rankImage = heroSetupper.rankImage;
        attackSystem = GetComponent<HeroAttackSystem>();
    }

    public void ChangeHeroExp(int expPoints)
    {
        if (expLevel < 15)
        {
            curExp += expPoints;
            expBar.fillAmount = (float)curExp / expToNextLevel[expLevel];
            if (curExp >= expToNextLevel[expLevel])
            {
                LevelUp();
            }
        }

    }

    private void LevelUp()
    {
        heroUpgradeInfos[expLevel].Upgrade();
        curExp -= expToNextLevel[expLevel];
        expLevel++;
        rankImage.sprite = rankSprites[expLevel];
    }
}
