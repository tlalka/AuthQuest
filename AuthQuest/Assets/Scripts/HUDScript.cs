using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{

    public GameObject InventoryA;

    public GameObject InventoryB;
    // Start is called before the first frame update
    void Start()
    {
        InventoryA = GameObject.Find("ItemImage1");

        InventoryB = GameObject.Find("ItemImage2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(GameObject weapon)
    {
        Image image = InventoryA.GetComponent<ImageConversion>();
        image.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

        // if (weapon.GetComponent<WeaponStats>().isRange == true)
    }
}
