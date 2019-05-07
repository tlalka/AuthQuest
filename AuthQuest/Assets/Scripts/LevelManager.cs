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
    public bool isShuttingDown;

    public GameObject[] bosses;

    public int levelcount;
    public bool bosslevel;

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
        levelcount = 1;
        bosslevel = false;
        player = GameObject.Find("Player");
        loading = GameObject.Find("Canvas");
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
        colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
        Debug.Log("level loaded");
        GameObject[] items2 = GameObject.FindGameObjectsWithTag("levelM");
        if (items2.Length > 1) {
            if (!OGobj)
            {
                Debug.Log("destroy this Level Manager");
                Destroy(gameObject);
            }
        }
        if (bosslevel)
        {
            BossStartup();
        }
        else
        {
            Startup();
        }
        isShuttingDown = false;
    }

    void BossStartup()
    {
        colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
        doors = GameObject.FindGameObjectsWithTag("Door");
        setDoors();
        player.GetComponent<PlayerController>().canMove = true;
        items = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < items.Length; i++)
        {
            Vector3 playerspawn = new Vector3(0, 0, 0);
            items[i].GetComponent<PlayerController>().MovePlayer(playerspawn);
            player.transform.position = playerspawn;
            //Debug.Log(items.Length);
            Debug.Log("move player to " + playerspawn);
        }
        loading.GetComponent<LoadingScreen>().renOff();
        LoadBoss();
    }

        void Startup()
    {
        loading.GetComponent<LoadingScreen>().renOn();
        Debug.Log("level start " + currentColor);

        //clear old stuff
        colorTiles.ClearAllTiles();
        Debug.Log("cleared old tiles, trying to build");

        Vector3 playerspawn;
        //if next level is boss level, only one door, no color
        Debug.Log("boss level " + bosslevel);
        if (levelcount == 3)
        {
            playerspawn = this.GetComponent<LevelGenerator>().BuildFloor(true, bosslevel);
            colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
            doors = GameObject.FindGameObjectsWithTag("Door");
            setOneDoor();
        }
        else
        {
            playerspawn = this.GetComponent<LevelGenerator>().BuildFloor(false, bosslevel);
            colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
            doors = GameObject.FindGameObjectsWithTag("Door");
            setDoors();
        }
        player.GetComponent<PlayerController>().canMove = true;
        Debug.Log("set doors");

        items = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<PlayerController>().MovePlayer(playerspawn);
            player.transform.position = playerspawn;
            //Debug.Log(items.Length);
            Debug.Log("move player to " + playerspawn);
        }
        loading.GetComponent<LoadingScreen>().renOff();
    }

    // Update is called once per frame
    void Update()
    {
        setTiles();//I don't want this here but whatever
    }

    void LoadBoss()
    {
        Debug.Log("generate boss");
        int pickone = UnityEngine.Random.Range(0, (bosses.Length - 1));
        Instantiate(bosses[pickone], new Vector3Int(100, 0, 0), Quaternion.identity);
    }

    void setOneDoor()
    {
        string color = "black";
        Color NewColor = Color.black;
        DoorProperties door = doors[0].GetComponent<DoorProperties>();
        switch (currentColor)
        {
            case "red":
                color = "red";
                NewColor = Color.red;//new Color(0f, 0f, 0f, 1f)
                break;
            case "yellow":
                color = "yellow";
                NewColor = Color.yellow;
                break;
            case "green":
                color = "green";
                NewColor = Color.green;
                break;
            case "blue":
                color = "blue";
                NewColor = Color.blue;
                break;
            case "pink":
                color = "pink";
                NewColor = pink;
                break;
            case "purple":
                color = "purple";
                NewColor = Color.magenta;
                break;
        }
        door.color = color;
        doors[0].GetComponent<SpriteRenderer>().color = NewColor;

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
                    NewColor = pink;
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
        //colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
        colorTiles.color = NewColor;
        //Debug.Log("set room color" + colorTiles.color);
    }

    public void LoadNewLevel(string color)
    {
        isShuttingDown = true;
        loading.GetComponent<LoadingScreen>().renOn();
        player.GetComponent<PlayerController>().canMove = false;
        for (int i = 0; i < doors.Length; i++)
        {
            //Debug.Log("destroy door");
            Destroy(doors[i]);
        }

        //if third room, load boss room instead
        if (levelcount == 3 )
        {
            player.GetComponent<PlayerStats>().LevelUp(currentColor);
            currentColor = color;
            Debug.Log("load new color " + currentColor);
            OGobj = true;
            levelcount = 1;
            bosslevel = true;
            //Debug.Log("set bosslevel to true");
            //destory our old grid
            GameObject grid = GameObject.Find("Grid");
            Destroy(grid);
            SceneManager.LoadScene("Boss-Level");//
        }
        else //load basic level
        {
            if (bosslevel) //level up enemies on boss levels
            {
                currentColor = color;
                Debug.Log("load new color " + currentColor);
                OGobj = true;
                GameObject grid = GameObject.Find("Grid");
                Destroy(grid);
                bosslevel = false;
                levelcount = 1;
                player.GetComponent<PlayerStats>().LevelUpEnemy();
            }
            else
            {
                player.GetComponent<PlayerStats>().LevelUp(currentColor);
                currentColor = color;
                Debug.Log("load new color " + currentColor);
                OGobj = true;
                levelcount++;
            }
            SceneManager.LoadScene("Basic-Level");
        }
    }

}
