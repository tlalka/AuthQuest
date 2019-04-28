using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    GameObject player;
    GameObject playerWeapon;
    GameObject playerHealthBar;
    public bool withinRangeOfPlayer;
    //public int multiplier = 5;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    private Rigidbody2D enemyRB2D;
    //public float range = 8;
    private int enemyDamage;
    private int playerDamage;
    //private int enemyHealth;
    private bool invincible;
    private SpriteRenderer spriteRenderer;
    GameObject enemyHealthBar;
    public int enemyAttack;
    public int enemyHealth = 1;
    public int enemySpeed;
    //public Component enemyStats;
    
    //PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
        withinRangeOfPlayer = false;
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        enemyRB2D = GetComponentInParent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
        playerWeapon = GameObject.Find("slash");
        enemyHealthBar = transform.GetChild(0).gameObject;
        if(gameObject.tag == "Boss") {
        enemyAttack = 12 + player.GetComponent<PlayerStats>().currentLevel;
        enemyHealth = 4 + player.GetComponent<PlayerStats>().currentLevel * 3;
        enemySpeed = 2;
        }
        else 
        {
        enemyAttack = 10 + player.GetComponent<PlayerStats>().currentLevel;
        enemyHealth = 1 + player.GetComponent<PlayerStats>().currentLevel;
        enemySpeed = 3;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        enemyPosition = this.gameObject.transform.position;
        CheckIfInRange();
        if (withinRangeOfPlayer)
        {
            enemyRB2D.AddForce((playerPosition - enemyPosition) * 3);
        }
        //player.GetComponent<Rigidbody2D>().Sleep();
    }
    void CheckIfInRange()
    {
        if (Vector2.Distance(enemyPosition, playerPosition) <= 30)
        {
            withinRangeOfPlayer = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
       // Debug.Log("enemy trigger " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            enemyDamage = (enemyAttack - player.GetComponent<PlayerStats>().MeleeDefense);
            playerHealthBar.GetComponent<HealthBarScript>().TakeDamage(enemyDamage);

            //Knockback();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Knockback();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("enemy trigger " + other.gameObject.name);

    }

    public void GemTouchedVirus(GameObject gem)
    {
        if (gem.gameObject.tag == "Weapon" && playerWeapon.GetComponent<slashing>().isAttacking)
        {
            if (!invincible)
            {
                invincible = true;
                
                enemyHealthBar.GetComponent<EnemyHealthBar>().TakeDamage(player.GetComponent<PlayerStats>().MeleeAttack);
                Knockback();
                StartCoroutine(FlashInvisible());

            }
        }
    }
    IEnumerator FlashInvisible()
    {
        //Debug.Log("flash");
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
            Debug.Log("flash");
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
        }
        spriteRenderer.enabled = true;
        invincible = false;
    }

    void Knockback()
    {
        Vector2 velocity = enemyRB2D.velocity;

        enemyRB2D.AddForce(-velocity * 300);
    }


}
