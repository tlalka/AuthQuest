using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFacingEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = spriteRenderer = GetComponentInParent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
