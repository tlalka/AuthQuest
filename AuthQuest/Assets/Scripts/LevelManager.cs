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
    public string currentColor;
    public GameObject[] items;

    public static LevelManager instance;

    Color blue = new Color(126f/255f, 160f / 255f, 224f / 255f);
    Color red = new Color(225f / 255f, 126f / 255f, 126f / 255f);
    Color green = new Color(133f / 255f, 225f / 255f, 126f / 255f);
    Color yellow = new Color(225f / 255f, 218f / 255f, 126f / 255f);
    Color purple = new Color(114f / 255f, 77f / 255f, 199f / 255f);
    Color pink = new Color(233f / 255f, 152f / 255f, 228f / 255f);

    private void Awake()
    {
        OGobj = false;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        loading = GameObject.Find("Canvas");
        colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
        Debug.Log("level manager start");
        DontDestroyOnLoad(gameObject);
        if (currentColor == null)
        {
            currentColor = "blue";
            Debug.Log("null color set");
        }
        Startup();
    }

    void OnLevelWasLoaded()
    {
        Debug.Log("level loaded");

        /* this gets rid of the old one and we want the old one
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        */
        GameObject[] items2 = GameObject.FindGameObjectsWithTag("levelM");
        if (items2.Length > 1) {
            if (!OGobj)
            {
                Destroy(gameObject);
            }
        }
        Startup();
    }

    void Startup()
    {
        loading.GetComponent<LoadingScreen>().renOn();
        Debug.Log("level start " + currentColor);

        //clear old stuff
        colorTiles.ClearAllTiles();        
        Vector3 playerspawn = this.GetComponent<LevelGenerator>().BuildFloor();

        colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
        doors = GameObject.FindGameObjectsWithTag("Door");
        setDoors();
        loading.GetComponent<LoadingScreen>().renOff();
        player.GetComponent<PlayerController>().canMove = true;

        items = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<PlayerController>().MovePlayer(playerspawn);
            player.transform.position = playerspawn;
            Debug.Log(items.Length);
            Debug.Log("move player to " + playerspawn);
        }
        loading.GetComponent<LoadingScreen>().renOff();
    }

    // Update is called once per frame
    void Update()
    {
        setTiles();//I don't want this here but whatever
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
            while (used[caseSwitch] && i < 5) //if i>6 we have more doors than colors and this loop would go forever
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
        Color NewColor = red;
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
        //Debug.Log("set room color" + colorTiles.color);
    }

    public void LoadNewLevel(string color)
    {
        loading.GetComponent<LoadingScreen>().renOn();
        player.GetComponent<PlayerController>().canMove = false;
        for (int i = 0; i < doors.Length; i++)
        {
            Debug.Log("destroy door");
            Destroy(doors[i]);
        }
        player.GetComponent<PlayerStats>().LevelUp(color);
      currentColor = color;
      Debug.Log("load new color " + currentColor);
      OGobj = true;
      SceneManager.LoadScene("leve-gen");//
    }

}
