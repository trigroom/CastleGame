using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionTextSetter : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text descriptionText, nameText, costText;
    [SerializeField] private string upgradeName, upgradeDescription, cost;

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = upgradeDescription;
        nameText.text = upgradeName;
        costText.text = cost + " $";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameText.text = "";
        descriptionText.text = "hover over the upgrade to see the description";
        costText.text = "";
    }

}
