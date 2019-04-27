using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    GameObject player;
    GameObject playerHealthBar;
    public float healthRegen = 1.5f;
    public GameObject oneUp;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHealthBar = GameObject.Find("HealthBar");
    }
    private void Update()
    {
        GameObject LevelManager = GameObject.Find("LevelManager");
        if (LevelManager.GetComponent<LevelManager>().bosslevel)
        {
            Destroy(this);
        }
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
