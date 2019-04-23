using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    GameObject player;
    GameObject playerHealthBar;
    public bool withinRangeOfPlayer;
    //public int multiplier = 5;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    private Rigidbody2D enemyRB2D;
    //public float range = 8;
    private float enemyDamage = .1f;
    public int enemyHealth;
    private bool invincible;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
        withinRangeOfPlayer = false;
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
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
        CheckIfInRange();
        if(withinRangeOfPlayer)
        {
            enemyRB2D.AddForce((playerPosition - enemyPosition) * 3);
        }
        player.GetComponent<Rigidbody2D>().Sleep();
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
            //StartCoroutine(FlashInvisible());
            Vector2 velocity = enemyRB2D.velocity;
            
            enemyRB2D.AddForce(-velocity * 300);
            
        }
        //else if(collision.gameObject.tag == "Weapon")
       // {
          // if(!invincible)
        // {
        //  [damage enemy]
        //  invincible = true;
        //  StartCoroutine(FlashInvisible());
        //  invincible = false;
        // }
       // }
    }
       IEnumerator FlashInvisible() {
           Debug.Log("flash");
           for(int i = 0; i < 5; i++)
           {
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(.1f);
                Debug.Log("flash");
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(.1f);
           }
            spriteRenderer.enabled = true;
       }
    
  //  void DamageEnemy()
  //  {

  //  }

}
