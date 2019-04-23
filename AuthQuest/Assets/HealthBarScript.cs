using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private Transform bar;
    private Vector3 offset;
    float currentScale;
    GameObject player;
    GameObject theCamera;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        theCamera = GameObject.Find("Main Camera");
        offset = transform.position - theCamera.transform.position;
        currentScale = 1f;
        bar = transform.Find("Bar");
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void Update()
    {
        transform.position = theCamera.transform.position + offset;
    }

    public void TakeDamage(float damageValueNormalized)
    {
        bar.localScale = new Vector3(currentScale - damageValueNormalized, 1f);
        //Debug.Log("damage value = " + damageValueNormalized);
       // Debug.Log("currentScale = " + currentScale);
       // Debug.Log((currentScale - damageValueNormalized) + " damage taken");
        currentScale = bar.localScale.x;
        if(currentScale <= .3)
        {
            bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.green;
        }
        if(currentScale <= 0)
        {
            bar.localScale = new Vector3(0f, 1f);
            //Destroy(player);
        }
    }
}
