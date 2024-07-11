using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChecker : MonoBehaviour
{
    public void OnMouseDown()
    {
        CharacterSelecter.instance.SpawnCharacter();
    }

}
