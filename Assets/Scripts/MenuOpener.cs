using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    [SerializeField]private GameObject curMenu;

    public void OpenMenu(GameObject menu)
    {
        curMenu.SetActive(false);
        curMenu = menu;
        curMenu.SetActive(true);
    }
}
