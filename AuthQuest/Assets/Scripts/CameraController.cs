using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private CameraController instance; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        offset = transform.position - player.transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
        {
        transform.position = player.transform.position + offset;
        }
    }
}
