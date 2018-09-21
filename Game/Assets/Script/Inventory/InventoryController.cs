using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InventoryController : MonoBehaviour {

    public static InventoryController inventoryController;

    List<GameObject> inventoryItems;

    private Hashtable itemDescriptionTable;
    //private Vector3 panelSize;

    // Use this for initialization
    void Awake()
    {
        // Singleton Pattern
        if (inventoryController == null)
        {
            DontDestroyOnLoad(gameObject);
            inventoryController = this;
        }
        else if (inventoryController != this)
        {
            Destroy(gameObject);
        }

        string itemDescriptionFileName = "/itemDescriptions.dat";
        string filePath = Application.persistentDataPath + itemDescriptionFileName;

        if (File.Exists(filePath))
        {
            // Read File
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            itemDescriptionTable = (Hashtable)bf.Deserialize(file);
            file.Close();
        } else {
            // Debug purposes
            itemDescriptionTable = new Hashtable() { 
                { "ring", "A ring with the power of all gods... or not" },
                { "ringEffects", "None"},
                { "backpack", "wow a large backpack!" },
                { "backpackEffects", "None"},
                { "belt", "I don't need a belt ... yet" },
                { "beltEffects", "None"},
                { "bomb", "?This seems dangerous?" },
                { "bombEffects", "None"},    
                { "book", "Wisdom!!! ... Title: ?!Thou shall not live?!" },
                { "bookEffects", "None"},
                { "bronze_coin", "Moneyyyyyyyyyyy --- RICHHHHHHHH" },
                { "bronze_coinEffects", "None"},
                { "clover", "Four leaf clover!? so lucky ..."},
                { "cloverEffects", "None"},
                { "feather", "Did it come from a bird?"},
                { "featherEffects", "None"}
            };
            // TODO: Generate an error
        }

        inventoryItems = new List<GameObject>();
        //panelSize = this.transform.Find("ItemPanel").GetComponent<RectTransform>().sizeDelta;
    }

    public void AddInventoryItem (string name, Sprite sprite) {

        GameObject item = new GameObject(name, typeof(CanvasRenderer));
        inventoryItems.Add(item);

        InventoryItem ii = item.AddComponent<InventoryItem>();
        ii.itemName = name;
        ii.sprite = sprite;
        ii.description = GetDescription(name);

        // Adds the item to the inventory (without using prefabs)
        item.layer = 11;
        Image img = item.AddComponent<Image>();
        img.sprite = sprite;
        img.preserveAspect = true;
        item.transform.SetParent(this.transform.Find("ItemPanel"));
        RectTransform rt = item.GetComponent<RectTransform>();
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(60.0f, 60.0f);
        // LocalPosition is with respect to the anchor of the parent
        // it does NOT set the PosX,Y,Z directly
        rt.localPosition = new Vector3(40.0f + 70.0f * ((inventoryItems.Count-1)%10),
                                       -40.0f - 70.0f * ((inventoryItems.Count-1)/10),
                                                   0.0f); 
        ColorBlock cb = item.AddComponent<Button>().colors;
        cb.highlightedColor = new Color(255, 170, 134);
    }

    /*
     * Getter for the itemDescriptionTable
     * 
     * Returns the description of an item to display to the user
     */
    string GetDescription(string itemName) {
        return (string)itemDescriptionTable[itemName];
    }

    /*
     * Retrieves the effect of the item for game purposes
     */
    string GetEffects(string itemName) {
        return (string)itemDescriptionTable[itemName + "Effects"];
    }
}
