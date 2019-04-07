using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    //public string LoadLevel;
    public GameObject levelManager;

    //public string areaTransitionName;

    //public AreaEntrance theEntrance;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("door start");
        levelManager = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            string color = this.GetComponent<DoorProperties>().color;
            levelManager.GetComponent<LevelManager>().LoadNewLevel(color);
        }
    }

}
