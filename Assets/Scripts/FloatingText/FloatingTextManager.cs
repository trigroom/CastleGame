using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField] private FloatingTextObject[] textObjects;
    public static FloatingTextManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public void SpawnFloatingText(string textToShow, float floatingTime, float floatingSpeed, Vector2 spawnPos, Color textColor)
    {
        foreach (var textObject in textObjects)
        {
            if(!textObject.isMoving) 
            {
                textObject.MoveText(textToShow, floatingTime, floatingSpeed, spawnPos, textColor);
                return;
            }
        }
    }
}
