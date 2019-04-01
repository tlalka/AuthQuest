using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController3 : MonoBehaviour
{
    GameObject player;
    GameObject playerHealthBar;
    public bool withinRangeOfPlayer;
    //public int multiplier = 5;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    private Rigidbody2D enemyRB2D;
    //public float range = 8;
    private float enemyDamage = .25f;
    public int enemyHealth;
    private Vector2 enemyDirection;
    private Vector2 lastCollisionPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        withinRangeOfPlayer = false;
        enemyRB2D = GetComponentInParent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(enemyHealth == 0)
        //{
            // Destroy(this.gameObject);
        //}
        playerPosition = player.transform.position;
        enemyPosition = this.gameObject.transform.position;
        enemyDirection = (playerPosition - enemyPosition).normalized;
        CheckIfInRange();
        if(withinRangeOfPlayer)
        {
            transform.position = Vector2.Lerp(enemyPosition, playerPosition, Time.deltaTime * 2);
        }
        player.GetComponent<Rigidbody2D>().Sleep();
        
       // if(lastCollisionPosition != null && Vector2.Distance(enemyPosition, lastCollisionPosition) >= 3)
      //  {
      //      enemyRB2D.velocity = Vector2.zero;
      //  }

    }
    void CheckIfInRange()
    {
        if(Vector2.Distance(enemyPosition, playerPosition) <= 8)
        {
            withinRangeOfPlayer = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealthBar.GetComponent<HealthBarScript>().TakeDamage(enemyDamage);
            Destroy(this.gameObject);
        }
        //else if(collision.gameObject.tag == "Weapon")
       // {
            //enemyHealth--;
       // }
    }
    //        Debug.Log("collision");
     //       enemyRB2D.AddForce(-enemyDirection * 600);
    //        lastCollisionPosition = playerPosition;
            
            
    //    }
   // }
}
