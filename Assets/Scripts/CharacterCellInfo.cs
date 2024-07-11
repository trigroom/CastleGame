using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCellInfo : MonoBehaviour
{
    public GameObject characterPrefab;
    public int cost;
    public TMP_Text costText;
    [SerializeField] private Image characterIconImage;


    public void SetCell(CharacterInfo charInfo)
    {
        characterPrefab = charInfo.characterPrefab;
        cost = charInfo.characterPrefab.GetComponent<InfoForSelectCell>().cost - characterPrefab.GetComponent<HealthSystem>().distributor.removedCost;
        costText.text = cost + " $";
        characterIconImage.sprite = charInfo.characterIcon;
    }
}
