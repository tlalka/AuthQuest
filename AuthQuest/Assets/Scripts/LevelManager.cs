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

    Color blue = new Color(126, 160, 224);
    Color red = new Color(225f, 126f, 126f, 1f);
    Color green = new Color(133, 225, 126);
    Color yellow = new Color(225, 218, 126);
    Color purple = new Color(114, 77, 199);
    Color pink = new Color(233, 152, 228);


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
        for (int i = 0; i < doors.Length; i++)
        {
            Color NewColor = Color.black;
            DoorProperties door = doors[i].GetComponent<DoorProperties>();
            string color = "black";

            bool[] used = { false, false, false, false, false, false };
            int caseSwitch = Random.Range(1, 6);
            int doornum = 0;
            while (used[caseSwitch] && doornum < 6)
            {
                caseSwitch = Random.Range(1, 6);
                doornum++;
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
