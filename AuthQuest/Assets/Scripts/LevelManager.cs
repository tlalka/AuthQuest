using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject player;
    public GameObject loading;
    public Tilemap colorTiles;
    public bool OGobj;
    private string currentColor;

    Color blue = new Color(126f/255f, 160f / 255f, 224f / 255f);
    Color red = new Color(225f / 255f, 126f / 255f, 126f / 255f);
    Color green = new Color(133f / 255f, 225f / 255f, 126f / 255f);
    Color yellow = new Color(225f / 255f, 218f / 255f, 126f / 255f);
    Color purple = new Color(114f / 255f, 77f / 255f, 199f / 255f);
    Color pink = new Color(233f / 255f, 152f / 255f, 228f / 255f);

    void Start()
    {
        OGobj = false;
        player = GameObject.Find("Player");
        loading = GameObject.Find("Canvas");
        Debug.Log("awake");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("levelM");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            Debug.Log("obj destoyed");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            if (currentColor == null)
            {
                currentColor = "blue";
                Debug.Log("null color set");
            }
            Startup();
        }
    }

    void OnLevelWasLoaded()
    {
        Debug.Log("level loaded");
        if (OGobj)
        {
            Startup();
        }
    }

    void Startup()
    {
        loading.GetComponent<LoadingScreen>().renOn();
        Debug.Log("level start " + currentColor);
        doors = GameObject.FindGameObjectsWithTag("Door");
        colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();

        //generate level
        colorTiles.ClearAllTiles();
        Vector3 playerspawn = this.GetComponent<LevelGenerator>().BuildFloor();
        Debug.Log(playerspawn);
        player.transform.position = playerspawn;
        setTiles();
        setDoors();
        loading.GetComponent<LoadingScreen>().renOff();
        player.GetComponent<PlayerController>().canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setDoors()
    {
        bool[] used = { false, false, false, false, false, false };
        for (int i = 0; i < doors.Length; i++)
        {
            Color NewColor = Color.black;
            DoorProperties door = doors[i].GetComponent<DoorProperties>();
            string color = "black";
            int caseSwitch = Random.Range(1, 6);
            while (used[caseSwitch] && i < 6) //if i>6 we have more doors than colors and this loop would go forever
            {
                caseSwitch = Random.Range(1, 6);
            }
            used[caseSwitch] = true;

            switch (caseSwitch)
            {
                case 1:
                    color = "red";
                    NewColor = Color.red;//new Color(0f, 0f, 0f, 1f)
                    break;
                case 2:
                    color = "yellow";
                    NewColor = Color.yellow;
                    break;
                case 3:
                    color = "green";
                    NewColor = Color.green;
                    break;
                case 4:
                    color = "blue";
                    NewColor = Color.blue;
                    break;
                case 5:
                    color = "pink";
                    NewColor = Color.white;
                    break;
                case 6:
                    color = "purple";
                    NewColor = Color.magenta;
                    break;
            }
            door.color = color;
            doors[i].GetComponent<SpriteRenderer>().color = NewColor;
        }
    }

    void setTiles()
    {
        Color NewColor = Color.black;
        switch (currentColor)
        {
            case "red":
                NewColor = red;
                break;
            case "yellow":
                NewColor = yellow;
                break;
            case "green":
                NewColor = green;
                break;
            case "blue":
                NewColor = blue;
                break;
            case "pink":
                NewColor = pink;
                break;
            case "purple":
                NewColor = purple;
                break;
        }
        colorTiles.color = NewColor;
        Debug.Log("set room color" + colorTiles.color);
    }

    public void LoadNewLevel(string color)
    {
        player.GetComponent<PlayerController>().canMove = false;
        currentColor = color;
      Debug.Log("load new color " + currentColor);
      OGobj = true;
      SceneManager.LoadScene("leve-gen");//
    }

}
