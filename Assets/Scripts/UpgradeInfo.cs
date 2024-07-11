using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeInfo : ScriptableObject
{
    public int cost;
    public int[] unlockableUpgradeButtons;
    public abstract void Upgrade();
}
