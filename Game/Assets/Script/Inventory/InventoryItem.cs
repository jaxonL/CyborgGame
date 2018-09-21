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

    void Start () {

        // When this is loaded in the inventory (by load file or pickup)
        // Ensure no other objects in the scene has the same name
        // NOTE: This assumes that every item is unique
        // Expensive implementation but runs once per scene (negligeable)
        CollectibleItem[] sceneItems = FindObjectsOfType<CollectibleItem>();
        foreach(CollectibleItem item in sceneItems) {
            if (item.name == itemName) {
                Destroy(item.gameObject);
            }
        }
    }

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
