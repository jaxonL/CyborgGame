using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMenuController : MonoBehaviour {

    public static InventoryMenuController inventoryMenuController;

	// Use this for initialization
	void Awake () {
        // Singleton Pattern
        if (inventoryMenuController == null)
        {
            DontDestroyOnLoad(gameObject);
            inventoryMenuController = this;
        }
        else if (inventoryMenuController != this)
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void TriggerInventoryMenu()
    {
        if (gameObject.activeSelf)
        {

            // Close UI
            gameObject.SetActive(false);
            // Enable controls (using timescale, up for change)
            Time.timeScale = 1.0f;

            // Deselect any buttons
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            // Disable controls (using timescale, up for change)
            Time.timeScale = 0.0f;
            // Open UI
            gameObject.SetActive(true);
        }
    }
}
