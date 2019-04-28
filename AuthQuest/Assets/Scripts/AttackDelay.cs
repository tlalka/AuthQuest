using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelay : MonoBehaviour
{
   
    private Transform bar;

    float currentScale;
    void Start()
    {
        bar = transform.Find("Bar");
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.green;
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
