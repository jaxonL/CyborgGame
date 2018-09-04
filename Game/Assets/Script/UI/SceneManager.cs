using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Scene changes from inputs
 */
public class SceneManager : MonoBehaviour {

    public Canvas pauseMenu;

    private bool pauseMenuIsOpen;

    private void Awake()
    {
        pauseMenuIsOpen = false;
        pauseMenu.enabled = false;
    }

    void Update () {

        // Escape Key Press
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenu();
        }
	}

    /*
     * Calls in Game Menu
     * 
     * Updates the current game status accordingly.
     */
    private void PauseMenu() {
        if (pauseMenuIsOpen) {
            // Close UI
            pauseMenu.enabled = false;
            pauseMenuIsOpen = false;
            // Enable musics
            // Enable controls (using timescale, up for change)
            Time.timeScale = 1.0f;
        }
        else {
            // Disable controls (using timescale, up for change)
            Time.timeScale = 0.0f;
            // Disable musics
            // Open UI
            pauseMenu.enabled = true;
            pauseMenuIsOpen = true;
        }
    }

    public void QuitGame() {
        // IF IN UNITY EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // ELSE 
        Application.Quit();
    }
}
