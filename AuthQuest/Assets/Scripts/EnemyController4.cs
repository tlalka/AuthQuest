using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController4 : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 enemyDirection;
    GameObject playerHealthBar;
    private Vector3 enemyPosition;
    private bool goRight;
    public int enemyHealth;
    private int enemyDamage = 1;
    private bool invincible;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D enemyRB2D;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        playerHealthBar = GameObject.Find("HealthBar");
        invincible = false;
        goRight = true;
     enemyDirection = Vector3.right;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(enemyHealth == 0)
        //{
            // Destroy(this.gameObject);
        //}
      transform.position += enemyDirection * speed * Time.deltaTime;
            enemyRB2D.velocity = Vector2.zero;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "thwomp")
        {
            if(goRight)
            {
            goRight = false;
            enemyDirection = Vector3.left;
            }
            else {
                goRight = true;
                enemyDirection = Vector3.right;
            }
        }
        else if(collision.gameObject.tag == "Player")
        {
            playerHealthBar.GetComponent<HealthBarScript>().TakeDamage(enemyDamage);
        }
        else if(collision.gameObject.tag == "Weapon")
        {
// if(!invincible)
        // {
        //  enemyHealth--;
        //  invincible = true;
        //  StartCoroutine(FlashInvisible());
        //  invincible = false;
        // }
        }
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
}
