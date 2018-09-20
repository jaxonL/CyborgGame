using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string itemName;
    public Sprite sprite;
    public string description;

    private Text displayedDescription;
    // Use this for initialization
    private void FindDisplayedText()
    {
        if (displayedDescription == null) {
            displayedDescription = GameObject.FindGameObjectWithTag("ItemDescription").GetComponent<Text>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        DisplayText();
    }

    public void OnPointerExit(PointerEventData eventData) {
        StopDisplayText();
    }

    private void DisplayText()
    {
        FindDisplayedText();
        displayedDescription.text = description;
    }
    private void StopDisplayText()
    {
        FindDisplayedText();
        displayedDescription.text = "";
    }
}
