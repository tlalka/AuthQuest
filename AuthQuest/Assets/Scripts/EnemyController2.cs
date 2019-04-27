using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    GameObject player;
    GameObject playerWeapon;
    GameObject playerHealthBar;
    public bool withinRangeOfPlayer;
    //public int multiplier = 5;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    private Vector2 enemyDirection;
    private Rigidbody2D enemyRB2D;
    //public float range = 8;
    private int enemyDamage;
    
    private Vector2 lastCollisionPosition;
    private bool invincible;
    private SpriteRenderer spriteRenderer;
    private GameObject enemyHealthBar;
    public int enemyAttack;
    public int enemyHealth;
    public int enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        //enemyHealth = 1f;
        invincible = false;
        withinRangeOfPlayer = false;
        enemyRB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
        playerWeapon = GameObject.Find("slash");
        enemyHealthBar = transform.GetChild(0).gameObject;
        enemyAttack = 12; // + [levelNumber];
        enemyHealth = 1; // + [levelNumber];
        enemySpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        enemyPosition = this.gameObject.transform.position;
        enemyDirection = (playerPosition - enemyPosition).normalized;
        CheckIfInRange();
        if (withinRangeOfPlayer)
        {
            transform.position = Vector2.MoveTowards(enemyPosition, playerPosition, Time.deltaTime * 4);
        }
        //player.GetComponent<Rigidbody2D>().Sleep();
        if (lastCollisionPosition != null && Vector2.Distance(enemyPosition, lastCollisionPosition) >= 3)
        {
            enemyRB2D.velocity = Vector2.zero;
        }
    }
    void CheckIfInRange()
    {
        if (Vector2.Distance(enemyPosition, playerPosition) <= 8)
        {
            withinRangeOfPlayer = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            enemyDamage = enemyAttack - player.GetComponent<PlayerStats>().MeleeDefense;
            playerHealthBar.GetComponent<HealthBarScript>().TakeDamage(enemyDamage);

            Knockback();
        }

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

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    IEnumerator FlashInvisible()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
        }
        spriteRenderer.enabled = true;
        invincible = false;
    }


    void Knockback()
    {
        enemyRB2D.AddForce(-enemyDirection * 600);
        lastCollisionPosition = playerPosition;
    }


}
