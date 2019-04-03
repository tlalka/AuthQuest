using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    public string LoadLevel;

    //public string areaTransitionName;

    //public AreaEntrance theEntrance;

    // Start is called before the first frame update
    void Start()
    {
        //theEntrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(LoadLevel);

            //PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

}
