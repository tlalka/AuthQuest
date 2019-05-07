using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarScript : MonoBehaviour
{
    private Transform bar;
    private Vector3 offset;
    float currentScale;
    GameObject player;
    GameObject theCamera;
    public GameObject DeathUI;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        theCamera = GameObject.Find("Main Camera");
        offset = transform.position - theCamera.transform.position;
        currentScale = 1f;
        bar = transform.Find("Bar");
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.green;
        DeathUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        //transform.position = theCamera.transform.position + offset;

        if (currentScale <= 0)
        {
            //Debug.Log("You are dead!");
            //DeathUI.gameObject.SetActive(true);
            //Destroy(player, 2f);
            //Time.timeScale = 0f;
            //bar.localScale = new Vector3(0f, 1f, 1f);
            //currentScale = bar.localScale.x;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void TakeDamage(int damageValue)
    {
        if (!player.GetComponent<PlayerController>().CheckIfInvincible())
        {
            float damageValueNormalized = ((float)damageValue) / ((float)(player.GetComponent<PlayerStats>().Health));
            bar.localScale = new Vector3(currentScale - damageValueNormalized, 1f, 1f);
            Debug.Log("damage value = " + damageValueNormalized);
            Debug.Log("currentScale = " + currentScale);
            Debug.Log((currentScale - damageValueNormalized) + " damage taken");
            currentScale = bar.localScale.x;
            if (currentScale <= .3)
            {
                bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (currentScale <= 0)
            {
                bar.localScale = new Vector3(0f, 1f, 1f);
                //Destroy(player);
            }
        }
    }

    public void HealthRegenerate(int regenValue)
    {
        Debug.Log("regen start");
        float regenValueNormalized = ((float)regenValue) / ((float)(player.GetComponent<PlayerStats>().Health));

        if ((currentScale + regenValueNormalized) > 1f)
        {
            bar.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            Debug.Log("add that health");
            bar.localScale = new Vector3(currentScale + regenValueNormalized, 1f, 1f);
            Debug.Log("regenValue = " + regenValue);
            Debug.Log("(float)regenValue = " + (float)regenValue);
            Debug.Log("health restored " + regenValueNormalized);
        }
        currentScale = bar.localScale.x;
    }
}
