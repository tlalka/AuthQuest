using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAppear : MonoBehaviour
{
    //[SerializeField] private Image customImage;
    GameObject player;
    public GameObject Image;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Image.gameObject.SetActive(true);
            Debug.Log("You have touched it!");
        } else
        {
            Debug.Log("OnCollisionEnter2D");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Image.gameObject.SetActive(false);
        }
    }

    public void clickCombo()
    {
        Debug.Log("You received the sword");
    }

    public void clickForceLock()
    {
        Debug.Log("The chest is broken!");
    }
}
