using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffInfo : MonoBehaviour
{
    public abstract void TakeBuff(Collider2D[] characters);
}
