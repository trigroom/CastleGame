using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterSetupper : MonoBehaviour
{
    [SerializeField]private GameObject[] characterCells;

    private void Awake()
    {
        SetupSelectedCharacters();
    }

    private void SetupSelectedCharacters()
    {
        for (int i = 0; i<UpgradesManager.instance.CheckSelectedCharactersCount(); i++)
        {
            characterCells[i].SetActive(true);
            characterCells[i].GetComponent<CharacterCellInfo>().SetCell(UpgradesManager.instance.selectedCharacters[i]); ;
        }
    }
}
