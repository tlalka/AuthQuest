using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAppear : MonoBehaviour
{
    //[SerializeField] private Image customImage;
    GameObject player;
    public GameObject chest;
    public GameObject Image;
    GameObject Crackchest;
    GameObject Lock1;
    GameObject Lock2;
    GameObject Lock3;
    GameObject Lock4;
    GameObject Lock5;
    GameObject Lock6;
    GameObject Lock7;
    GameObject Lock8;
    GameObject Lock11;
    GameObject Lock21;
    GameObject Lock31;
    GameObject Lock41;
    GameObject Lock51;
    GameObject Lock61;
    GameObject Lock71;
    GameObject Lock81;
    GameObject successscreen;
    GameObject failurescreen;
    private bool isCrackChest = false;
    private bool lock1cracked = false;
    private bool lock2cracked = false;
    private bool lock3cracked = false;
    private bool lock4cracked = false;
    private bool lock5cracked = false;
    private bool lock6cracked = false;
    private bool lock7cracked = false;
    private bool lock8cracked = false;
    private bool success = false;

    public int chest1choice = 1;
    public bool chest1success = true;
    public string chest1item = "Armadyl Godsword";

    //up down down up left right left up

    void Awake()
    {
        player = GameObject.Find("Player");
        chest = GameObject.Find("chest");
        Image = GameObject.Find("Image");
        Crackchest = GameObject.Find("Crackchest");
        Lock1 = GameObject.Find("lock1");
        Lock2 = GameObject.Find("lock2");
        Lock3 = GameObject.Find("lock3");
        Lock4 = GameObject.Find("lock4");
        Lock5 = GameObject.Find("lock5");
        Lock6 = GameObject.Find("lock6");
        Lock7 = GameObject.Find("lock7");
        Lock8 = GameObject.Find("lock8");
        Lock11 = GameObject.Find("lock1 (1)");
        Lock21 = GameObject.Find("lock2 (1)");
        Lock31 = GameObject.Find("lock3 (1)");
        Lock41 = GameObject.Find("lock4 (1)");
        Lock51 = GameObject.Find("lock5 (1)");
        Lock61 = GameObject.Find("lock6 (1)");
        Lock71 = GameObject.Find("lock7 (1)");
        Lock81 = GameObject.Find("lock8 (1)");
        successscreen = GameObject.Find("Successscreen");
        failurescreen = GameObject.Find("Failurescreen");
        //Debug.Log(Image);
      
    }

    void Update()
    {
        if (isCrackChest == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Lock1 successfully cracked!");
                Lock11.gameObject.SetActive(true);
                lock1cracked = true;
            }
        }

        if (lock1cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Lock21.gameObject.SetActive(true);
                Debug.Log("Lock2 successfully cracked!");
                lock2cracked = true;
            }
        }

        if (lock2cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Lock81.gameObject.SetActive(true);
                Debug.Log("Lock8 successfully cracked!");
                lock8cracked = true;
            }
        }

        if (lock8cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Lock71.gameObject.SetActive(true);
                Debug.Log("Lock7 successfully cracked!");
                lock7cracked = true;
            }
        }

        if (lock7cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Lock61.gameObject.SetActive(true);
                Debug.Log("Lock6 successfully cracked!");
                lock6cracked = true;
            }
        }

        if (lock6cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Lock51.gameObject.SetActive(true);
                Debug.Log("Lock5 successfully cracked!");
                lock5cracked = true;
            }
        }

        if (lock5cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Lock41.gameObject.SetActive(true);
                Debug.Log("Lock4 successfully cracked!");
                lock4cracked = true;
            }
        }

        if (lock4cracked == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Lock31.gameObject.SetActive(true);
                Debug.Log("Lock3 successfully cracked!");
                lock3cracked = true;
            }
        }

        if (lock3cracked == true)
        {
            success = true;
            Crackchest.gameObject.SetActive(false);
            Destroy(chest);
            //StartCoroutine(ShowSuccessMessage());
            Time.timeScale = 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Image = GameObject.FindGameObjectWithTag("Image");
            //Image = GameObject.Find("Image");
            Image = GameObject.Find("Image");
            Image.gameObject.transform.position = new Vector3(0, 0, 0);
            Debug.Log(Image);
            Debug.Log("You have touched it!");
            //Image.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("OnCollisionEnter2D");
        }
    }
    /*
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            //Image.gameObject.SetActive(false);
            Image = GameObject.Find("Image");
            Image.gameObject.transform.position = new Vector3(0, 0, 0);
            Time.timeScale = 1f;
        }
    }*/

    public void clickCombo()
    {
        chest = GameObject.Find("chest");
        chest1choice = 1;
        //Image.gameObject.SetActive(false);
        Image = GameObject.Find("Image");
        Image.gameObject.transform.position = new Vector3(1000, 0, 0);
        Crackchest.gameObject.SetActive(true);
        Debug.Log("successfully opened Crack Chest interface");
        isCrackChest = true;
        chest1item = "Armadyl Godsword";
        chest1success = true;
    }

    public void clickForceLock()
    {
        chest = GameObject.Find("chest");
        chest1choice = 2;
        var number = Random.Range(1, 100);
        if (number <= 50)
        {
            chest1item = "Health Potion";
            chest1success = false;
            //StartCoroutine(ShowFailureMessage());
            Debug.Log("The chest has been broken");
        }
        else
        {
            //StartCoroutine(ShowSuccessMessage());
            chest1item = "Armadyl Godsword";
            chest1success = true;
            Debug.Log("The chest rewards you with an item!");
        }

        //Image.gameObject.SetActive(false);

        Image = GameObject.Find("Image");
        Image.gameObject.transform.position = new Vector3(1000, 0, 0);
        Destroy(chest);
        Time.timeScale = 1f;
    }

    IEnumerator ShowSuccessMessage()
    {
        Debug.Log("disappeared");
        successscreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        successscreen.gameObject.SetActive(false);
    }

    IEnumerator ShowFailureMessage()
    {
        Debug.Log("disappeared");
        failurescreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        failurescreen.gameObject.SetActive(false);
    }

    public int getChestChoice()
    {
        return chest1choice;
    }

    public bool getChestSuccess()
    {
        return chest1success;
    }

    public string getChestItem()
    {
        return chest1item;
    }

}
