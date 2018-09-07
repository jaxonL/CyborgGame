using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 * GameController manages input affecting the game state.
 * 
 * Only one GameController should exist at a time
 */
public class GameController : MonoBehaviour {

    public static GameController gameController;

    private string _persistentFilesPath;

    private float startTime;
    private float lastLoadTime;
    private float gameplayTime;
    private SaveFileData saveFileData;

    void Awake()
    {
        // Singleton Pattern
        if (gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }
        else if (gameController != this)
        {
            Destroy(gameObject);
        }
        startTime = Time.time;
        lastLoadTime = 0.0f;
        gameplayTime = 0.0f;
        _persistentFilesPath = Application.persistentDataPath;
    }

    void Update () {

        // Escape Key Press
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenuController.pauseMenuController.TriggerPauseMenu();
        }

	}

    public void QuitGame() {
        // IF IN UNITY EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // ELSE 
        Application.Quit();
    }

    public void SaveGame(string saveSlotNumber) {

        // File name and path setup
        string fileName = "/saveFile" + saveSlotNumber + ".dat";
        string filePath = _persistentFilesPath + fileName;

        // If file already exits ...
        if (File.Exists(filePath)) {
            // ... Prompt User ...
            // TODO: Modal Window - Return if canceled
        }
        FileStream file = File.Create(filePath);


        // Saved Data Initialization
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SaveFileData data = new SaveFileData
        {
            // TODO: Add Data to be saved
            sceneName = SceneManager.GetActiveScene().name,
            characterPositionX = player.transform.position.x,
            characterPositionY = player.transform.position.y,
            characterPositionZ = player.transform.position.z,
            totalPlayTime = Time.time - startTime - lastLoadTime + gameplayTime
        };

        // Write File 
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Game Saved Successfully!");
        Debug.Log("File Stored in path: " + filePath);
    }

    public void LoadGame(string saveSlotNumber) {
        // File name and path setup
        string fileName = "/saveFile" + saveSlotNumber + ".dat";
        string filePath = _persistentFilesPath + fileName;

        if (File.Exists(filePath)) {
            // Read File
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            saveFileData = (SaveFileData) bf.Deserialize(file);
            file.Close();

            // TODO: Prompt user to load file

            // Load Data into the game
            SceneManager.LoadScene(saveFileData.sceneName);
            // TODO: Add Data to be loaded
            StartCoroutine(LoadDataOnceSceneLoaded(saveFileData));

            gameplayTime = saveFileData.totalPlayTime;
            lastLoadTime = Time.time;
        }

    }

    /*
     * Loads data once scene is loaded
     * 
     * Note: This function is not absolute especially if scene 
     * takes a long time to load.
     */
    IEnumerator LoadDataOnceSceneLoaded(SaveFileData data) {

        // Wait for scene to load
        yield return new WaitForSecondsRealtime(0.1f);

        // TODO: Add Data to be loaded ONCE SCENE IS DONE LOADING
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = new Vector3(data.characterPositionX,
                                                data.characterPositionY,
                                                data.characterPositionZ);
        player.transform.position = playerPos;

        Debug.Log("Data Loaded");
        // Stops when the player is at the right position (reference point)
        if (player.transform.position == playerPos) {
            yield break;
        }
    }
}


[Serializable]
/*
 * Struct containing all the data needed to load a gameplay
 */
struct SaveFileData {
    // TODO: Add Data to be saved
    public string sceneName;
    public float characterPositionX;
    public float characterPositionY;
    public float characterPositionZ;
    public float totalPlayTime;
}