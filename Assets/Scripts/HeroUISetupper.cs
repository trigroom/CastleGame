using UnityEngine;
using UnityEngine.UI;

public class HeroUISetupper : MonoBehaviour
{
    [SerializeField] public Image[] reloadImages;
    [SerializeField] public GameObject[] abilityContainers;
    [SerializeField] public Image hpBarImage, expBarImage, rankImage;

    private void Start()
    {
        abilityContainers[1].SetActive(false);
        abilityContainers[2].SetActive(false);
    }
}
