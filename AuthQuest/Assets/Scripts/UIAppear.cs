using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    [SerializeField] private Image customImage;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            customImage.enabled = true;
            
        } else
        {
            Debug.Log("OnCollisionEnter2D");
        }
    }

    //void OnCollisionExit(Collision2D col)
    //{
    //    if (col.gameObject.name == "Player")
    //    {
    //      customImage.enabled = true;
    //    }
    //}
}
