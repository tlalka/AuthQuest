using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Save()
    {
        Debug.Log("saved!");
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        SaveObject saveObject = new SaveObject
        {
            goldAmount = 5,
            playerPosition = playerPosition,
            chestMethod = 1,
            getItem = false,
        };
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.json", json);
        Debug.Log(json);
    }

    public void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.json");

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        GameObject.FindGameObjectWithTag("Player").transform.position = saveObject.playerPosition;
    }

    public class SaveObject
    {
        public int goldAmount;
        public Vector3 playerPosition;
        public bool getItem;
        public int chestMethod;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Debug.Log("Quit!");
        Application.Quit();
    }
}
