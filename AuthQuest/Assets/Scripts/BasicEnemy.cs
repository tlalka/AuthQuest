using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        //levelmanager = GameObject.FindWithTag("levelM");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        int drop = UnityEngine.Random.Range(0, 3);
        if (drop == 1)
        {
            Debug.Log("drop health");
            Vector3 position = this.transform.position;
            Instantiate(heart, position, Quaternion.identity);
        }
    }
}
