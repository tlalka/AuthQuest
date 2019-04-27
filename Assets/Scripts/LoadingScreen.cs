using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject Mcamera;
    private Vector3 offset;
    public Renderer rend;
    public static LoadingScreen instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Mcamera = GameObject.Find("Main Camera");
        offset = transform.position - Mcamera.transform.position;
        rend = GameObject.Find("temp loading screen").GetComponent<Renderer>(); //GetComponent<Renderer>();
        renOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mcamera != null)
        {
            transform.position = Mcamera.transform.position + offset;
        }
    }
    public void renOn()
    {
        rend.enabled = true;
    }
    public void renOff()
    {
        rend.enabled = false;
    }
}
