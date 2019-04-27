using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    GameObject player;
    GameObject playerHealthBar;
    public bool withinRangeOfPlayer;
    //public int multiplier = 5;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    private Vector2 enemyDirection;
    private Rigidbody2D enemyRB2D;
    //public float range = 8;
    private float enemyDamage = .1f;
    public int enemyHealth;
    private Vector2 lastCollisionPosition;
    private bool invincible;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        withinRangeOfPlayer = false;
        enemyRB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
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
            transform.position = Vector2.MoveTowards(enemyPosition, playerPosition, Time.deltaTime * 4);
        }
        //player.GetComponent<Rigidbody2D>().Sleep();
        if(lastCollisionPosition != null && Vector2.Distance(enemyPosition, lastCollisionPosition) >= 3)
        {
            enemyRB2D.velocity = Vector2.zero;
        }
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
            
            enemyRB2D.AddForce(-enemyDirection * 600);
            lastCollisionPosition = playerPosition;
        }
        //else if(collision.gameObject.tag == "Weapon")
       // {
          // if(!invincible)
        // {
        //  enemyHealth--;
        //  invincible = true;
        //  StartCoroutine(FlashInvisible());
        //  invincible = false;
        // }
       // }

       //IEnumerator FlashInvisible() {
          // for(int i = 0, i < 5, i++)
         //  {
             //   spriteRenderer.enabled = true;
             //   yield return WaitForSeconds(.1f);
             //   spriteRenderer.enabled = false;
             //   yield return WaitForSeconds(.1f);
         //  }
           // spriteRenderer.enabled = true;
       //}
    }

    
}
