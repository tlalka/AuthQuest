using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public Inventory1 Inventory1;
    public Inventory2 Inventory2;

    //!!!!!!!!!!!!!!!!!
    public GameObject InventoryA;
    //!!!!!!!!!!!!!!

    // Start is called before the first frame update
    void Start()
    {
        /* Inventory1 = GameObject.Find("Inventory 1").GetComponent<Inventory1>();
        Inventory2 = GameObject.Find("Inventory 2").GetComponent<Inventory2>();
        */
        //Inventory1.ItemAdded += InventoryScript_ItemAdded;

        //!!!!!!!!!!!!
        InventoryA = GameObject.Find("ItemImageA");
        //!!!!!!!!!!!!!
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory1Panel");
        foreach(Transform slot in inventoryPanel)
        {
            //Border... Image
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            //We found the empty slot
            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                break;
            }
        }
    }

    //!!!!!!!!!!!!!!
    public void changeweapon(GameObject weapon)
    {
        Image image = InventoryA.GetComponent<Image>();
        image.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

        //if(weapon.GetComponent<WeaponStats>().isRange == true)
    }
    //!!!!!!!!!!!!!!!!!!!!!!

    // Update is called once per frame
    void Update()
    {
        
    }
}
