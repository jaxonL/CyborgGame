using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Pause Menu control 
 * 
 * Only one PauseMenu script should exist at a time
 * This script should be attached to the Canvas of the Pause Menu
 */
public class PauseMenuController : MonoBehaviour
{

    public static PauseMenuController pauseMenuController;

    void Awake()
    {
        // Singleton Pattern
        if (pauseMenuController == null)
        {
            DontDestroyOnLoad(gameObject);
            pauseMenuController = this;
        }
        else if (pauseMenuController != this)
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    /*
    * Calls in Game Menu
    * 
    * Updates the current game status accordingly.
    */
    public void TriggerPauseMenu()
    {
        if (gameObject.activeSelf)
        {
            string activeMenu = GetActiveMenu();
            if (activeMenu == "BaseMenu")
            {
                // Close UI
                gameObject.SetActive(false);
                // Enable musics
                // Enable controls (using timescale, up for change)
                Time.timeScale = 1.0f;
            }
            else
            {
                SetActiveMenu("BaseMenu");
            }
            // Deselect any buttons
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            // Disable controls (using timescale, up for change)
            Time.timeScale = 0.0f;
            // Disable musics
            // Open UI
            SetActiveMenu("BaseMenu");
            gameObject.SetActive(true);
        }
    }

    public void SetActiveMenu(string menuTag)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == menuTag)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    /*
     * Return the current active menu
     * 
     * Note: There should always be an active menu
     */
    private string GetActiveMenu() {
        foreach (Transform child in transform) {
            if (child.gameObject.activeSelf) return child.gameObject.tag;
        }
        return null;
    }
}
