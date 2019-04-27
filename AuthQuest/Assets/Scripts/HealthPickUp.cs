﻿using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    GameObject player;
    GameObject playerHealthBar;
    public int healthRegen = 20;
    public GameObject oneUp;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthBar.GetComponent<HealthBarScript>().HealthRegenerate(healthRegen);
            Instantiate(oneUp,
                new Vector3(transform.position.x,
                            transform.position.y + 3f,
                            transform.position.z),
                            Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
