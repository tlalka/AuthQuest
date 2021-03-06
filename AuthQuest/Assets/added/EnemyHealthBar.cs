﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Transform bar;
    private Vector3 offset;
    float currentScale;
    private Transform barBackground;
    private Transform barBorder;
    GameObject enemy;
    Component enemysStats;
    //GameObject theCamera;
    //public GameObject DeathUI;
    // Start is called before the first frame update
    private void Start()
    {
        enemy = transform.parent.gameObject;
        //theCamera = GameObject.Find("Main Camera");
        transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 2);
        offset = transform.position - enemy.transform.position;
        //offset.x = transform.position.x - enemy.transform.position.x;
        //offset.y = transform.position.y - (enemy.transform.position.y + 1);
        currentScale = 1f;
        bar = transform.GetChild(1);
        barBackground = transform.GetChild(0);
        barBorder = transform.GetChild(2);
        bar.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        barBackground.GetComponent<SpriteRenderer>().enabled = false;
        barBorder.GetComponent<SpriteRenderer>().enabled = false;
        

    }

    private void Update()
    {
        transform.position = enemy.transform.position + offset;

        
    }

    public void TakeDamage(int damageValue)
    {
        float damageValueNormalized = 1f;
        if(enemy.tag == "Virus1" || enemy.tag == "Boss")
        {
        damageValueNormalized = ((float)damageValue)/((float)(enemy.GetComponent<EnemyController>().enemyHealth));
        }
        else
        {
        damageValueNormalized = ((float)damageValue)/((float)(enemy.GetComponent<EnemyController2>().enemyHealth));
        }
        bar.localScale = new Vector3(currentScale - damageValueNormalized, 1f);
        //Debug.Log("damage value = " + damageValueNormalized);
       // Debug.Log("currentScale = " + currentScale);
       // Debug.Log((currentScale - damageValueNormalized) + " damage taken");
        currentScale = bar.localScale.x;
        if(currentScale <= .3)
        {
            bar.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            bar.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        }
        if(currentScale < 1f) {
            bar.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            barBackground.GetComponent<SpriteRenderer>().enabled = true;
            barBorder.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(currentScale <= 0)
        {
            bar.localScale = new Vector3(0f, 1f);
            Destroy(enemy);
        }
    }
}