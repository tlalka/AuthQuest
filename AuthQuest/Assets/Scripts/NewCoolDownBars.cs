using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCoolDownBars : MonoBehaviour
{

    GameObject player;
    private CoolItDownBro BarHealth;
    private Image barImage;
    
    slashing isAttacking;

    private void Awake() {
        player = GameObject.Find("Player");
        barImage = transform.Find("Bar").GetComponent<Image>();

        BarHealth = new CoolItDownBro();
    }

        private void Update() {
            BarHealth.Update();

            barImage.fillAmount = BarHealth.GetBarNormalized();
            //BarHealth.GetBarNormalized();
        }
    }


    public class CoolItDownBro {
        public const int Cooling_Max = 1;
       
        public float BarAmount;
        
        private float BarDecreaseAmount;
        
        public CoolItDownBro() {
            BarAmount = 0;
            BarDecreaseAmount = .3f;
            
        }

        public void Update() {
        //Debug.Log("BarDecreaseAmount: " + BarDecreaseAmount);
        //Debug.Log("BarAmount: " + BarAmount);
            BarAmount -= BarDecreaseAmount * Time.deltaTime;
        //Debug.Log("BarAmount: " + BarAmount);
            BarAmount = Mathf.Clamp(BarAmount, 0f, Cooling_Max);

         }

        public void BarRegenHealth(int amount) {

        if (BarAmount <= amount) {
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

