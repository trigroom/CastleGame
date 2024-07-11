using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToSelectHero : MonoBehaviour
{
    [SerializeField] private int characterNumber;
    [SerializeField] private GameObject border;
    [SerializeField] private Sprite selectedBorder;
    [SerializeField]private Image borderImage;
    private bool isSelect;

    private void Start()
    {
        borderImage = border.GetComponent<Image>();
        CheckCharacterToLock();
    }
    private void CheckCharacterToLock()
    {
        if (!UpgradesManager.instance.unlockedCharacters[characterNumber])
            GetComponent<Button>().enabled = false;
        else if (isSelect)
            GetComponent<Button>().enabled = true;
        else
        {
            border.SetActive(false);
            GetComponent<Button>().enabled = true;
        }
    }

    public void DeselectHero()
    {
        borderImage.gameObject.SetActive(false);
        isSelect = false;
    }
    public void SelectHero()
    {
        if (!isSelect)
        {
        UpgradesManager.instance.CheckCurrentSelectedHero(characterNumber);
        borderImage.sprite = selectedBorder;
        borderImage.gameObject.SetActive(true);
        isSelect = true;
        }

    }
}
