using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HUDScript : MonoBehaviour
{
    public static HUDScript instance;
    public GameObject InventoryA;
    public GameObject InventoryB;
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
        InventoryA = GameObject.Find("ItemImage1");

        InventoryB = GameObject.Find("ItemImage2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(GameObject weapon)
    {

        if (weapon.GetComponent<WeaponStats>().isRange == true) {
            Image image2 = InventoryB.GetComponent<Image>();
            image2.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        } else {
            Image image1 = InventoryA.GetComponent<Image>();
        image1.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
