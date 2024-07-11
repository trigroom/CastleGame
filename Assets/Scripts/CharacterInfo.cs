using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterInfo", order = 1)]
public class CharacterInfo : ScriptableObject
{
    public GameObject characterPrefab;
    public Sprite characterIcon;
}
