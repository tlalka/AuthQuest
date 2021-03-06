﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCoolDownBars : MonoBehaviour
{

    GameObject player;
    public CoolItDownBro BarHealth;
    private Image barImage;
    
    slashing isAttacking;
    public bool iscoolingdown;

    private void Awake() {
        player = GameObject.Find("Player");
        barImage = transform.Find("Bar").GetComponent<Image>();

        BarHealth = new CoolItDownBro();
        iscoolingdown = false;
    }

        private void Update() {
            BarHealth.Update();

            barImage.fillAmount = BarHealth.GetBarNormalized();
            if (BarHealth.GetBarNormalized() > 0) {
                iscoolingdown = true;
            }
            if (BarHealth.GetBarNormalized() == 0) {
                iscoolingdown = false;
            }
        }
    }


    public class CoolItDownBro {
        public const int Cooling_Max = 1;
       
        public float BarAmount;
        
        private float BarDecreaseAmount;
        GameObject player;



        public CoolItDownBro() {
            player = GameObject.Find("Player");
            BarAmount = 0;
            BarDecreaseAmount = .5f;
            //Debug.Log(BarDecreaseAmount);
        //.5f is the slowest, big numbers go faster

        }

        public void Update() {
        //Debug.Log("BarDecreaseAmount: " + BarDecreaseAmount);
        //Debug.Log("BarAmount: " + BarAmount);
            BarDecreaseAmount = (float)player.GetComponent<PlayerController>().meleeWeapon.GetComponent<WeaponStats>().weaponSpeed / 2;
            BarAmount -= BarDecreaseAmount * Time.deltaTime;
        //Debug.Log("BarAmount: " + BarAmount);
            BarAmount = Mathf.Clamp(BarAmount, 0f, Cooling_Max);


         }

        public void BarRegenHealth(int amount) {

        if (BarAmount == 0) {
            BarAmount += amount;
        }
        //if (isAttacking = true) {
        //BarAmount =+ Cooling_Max;
        //}
    }
    public float GetBarNormalized() {
        return BarAmount / Cooling_Max;
    }
}

