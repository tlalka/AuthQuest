using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    //these must be set
    public string weaponName;
    public int weaponAttack;
    public int weaponSpeed;//controls attack duration and cooldown, 
    public Quaternion rotation;
    public Vector3 scale;
    public bool isRange;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rotation = transform.rotation;
        scale = transform.localScale;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("picked up weapon");
            player.GetComponent<PlayerController>().EquipWeapon(this.gameObject);
            //now the weapon is equipped, we need to move it to the UI
        }
    }
}
