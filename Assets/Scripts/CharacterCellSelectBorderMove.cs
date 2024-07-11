using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCellSelectBorderMove : MonoBehaviour
{
    public void MoveBorder(float newXPos)
    {
        gameObject.transform.localPosition = new Vector2(newXPos, 0);
    }
}
