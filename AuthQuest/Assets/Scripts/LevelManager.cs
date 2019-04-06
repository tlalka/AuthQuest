using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public GameObject[] doors;
    public Tilemap colorTiles;
    public string currentColor;

    Color blue = new Color(126f/255f, 160f / 255f, 224f / 255f);
    Color red = new Color(225f / 255f, 126f / 255f, 126f / 255f);
    Color green = new Color(133f / 255f, 225f / 255f, 126f / 255f);
    Color yellow = new Color(225f / 255f, 218f / 255f, 126f / 255f);
    Color purple = new Color(114f / 255f, 77f / 255f, 199f / 255f);
    Color pink = new Color(233f / 255f, 152f / 255f, 228f / 255f);


    void Start()
    {
        //currentColor = "blue";
        setTiles();
        setDoors();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setDoors()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
        bool[] used = { false, false, false, false, false, false };
        for (int i = 0; i < doors.Length; i++)
        {
            Color NewColor = Color.black;
            DoorProperties door = doors[i].GetComponent<DoorProperties>();
            string color = "black";
            int caseSwitch = Random.Range(1, 6);
            while (used[caseSwitch] && i < 6) //if i>6 we have more doors than colors and this loop will go forever
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
            Debug.Log(doors[i].GetComponent<SpriteRenderer>().color);
        }
    }

    void setTiles()
    {
        //colorTiles = Tilemap.FindObjectsOfTypeAll; //i don't know how to find the tilemap
        Color NewColor = Color.black;
        
        switch (currentColor)
        {
            case "red":
                NewColor = red;//new Color(0f, 0f, 0f, 1f)
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
        Debug.Log(colorTiles.color);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            GameObject obj = other.attachedRigidbody.gameObject;
            //SceneManager.LoadScene(LoadLevel);
        }
    }

}
