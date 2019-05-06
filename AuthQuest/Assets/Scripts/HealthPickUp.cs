using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHealthBar;
    private int healthRegen = 20;
    public GameObject oneUp;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
    }

    private void OnLevelLoaded()
    {
        Destroy(this);
    }

    private void Update()
    {
    }

    /*   void OnCollisionEnter2D(Collision2D collision)
    {
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("health pickup triggered");
            playerHealthBar.GetComponent<HealthBarScript>().HealthRegenerate(healthRegen);
            Debug.Log("healthRegen = " + healthRegen);
            Instantiate(oneUp,
                new Vector3(transform.position.x,
                            transform.position.y + 3f,
                            transform.position.z),
                            Quaternion.identity);

            Destroy(gameObject);
        }
    } */
    

      void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("health pickup triggered");
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
