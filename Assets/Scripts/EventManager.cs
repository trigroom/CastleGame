using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action MapEnded;
    public static event Action MapStarted;
    public static event Action AllDataLoaded;
    public static event Action DataUpgradeManagerLoaded;

    public static void OnMapEnded() => MapEnded?.Invoke();

    public static void OnMapStarted() => MapStarted?.Invoke();

    public static void OnAllDataLoaded() => AllDataLoaded?.Invoke();

    public static void OnDataUpgradeManagerLoaded() => DataUpgradeManagerLoaded?.Invoke();
}
