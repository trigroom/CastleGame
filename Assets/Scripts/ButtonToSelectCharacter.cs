using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToSelectCharacter : MonoBehaviour
{
    [SerializeField]private int characterNumber, currentCellNumber;
    [SerializeField] private GameObject border;
    [SerializeField] private Sprite selectedBorder;
    private Image characterIcon;
    private bool isSelect;

    private void Start()
    {
        characterIcon = GetComponentsInChildren<Image>()[1];
       // Invoke("CheckCharacterToLock",0.7f);
       CheckCharacterToLock();
    }

    private void CheckCharacterToLock()
    {
        if (!UpgradesManager.instance.unlockedCharacters[characterNumber])
        {
            GetComponent<Button>().enabled = false;
            characterIcon.color = Color.black;
        }
        else if(isSelect)
            GetComponent<Button>().enabled = true;
        else
        {
            border.SetActive(false);
            GetComponent<Button>().enabled = true;
        }
    }

    public void SelectCharacter()
    {
        if (!isSelect)
        {
            currentCellNumber = UpgradesManager.instance.SelectCharacter(UpgradesManager.instance.characters[characterNumber]);
            if (currentCellNumber != -1 ) 
            { 
                isSelect = true;
                border.GetComponent<Image>().sprite = selectedBorder;
                border.SetActive(true);
            }
        }
        else
        {
            UpgradesManager.instance.DeselectCharacter(currentCellNumber);
            isSelect = false;
            border.SetActive(false);
        }
    }
}
