using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerController : MonoBehaviour
{
    //Jeff's

    public Rigidbody2D theRB;
    public float moveSpeed;
    public bool isAttacking;
    public bool IsMoving;
    protected Coroutine attackRoutine;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;

    public Image customImage; //get this sword out of here. not the right place for it
    public Image sword;

    public bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        isAttacking = false;
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        moveSpeed = 7;
    }

    // Update is called once per frame
    void Update() //characer movment
    {
        movePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            attackRoutine = StartCoroutine(Attack());
            // StartCoroutine(Attack());

        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    public void Save()
    {
        Debug.Log("saved!");
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        UIAppear uia = new UIAppear();
        int chest1choice = uia.chest1choice;
        bool chest1success = uia.chest1success;
        string chest1item = uia.chest1item;

        SaveObject saveObject = new SaveObject
        {
            goldAmount = 5,
            playerPosition = playerPosition,
            chest1choice = chest1choice,
            chest1success = chest1success,
            chest1item = chest1item
        };
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.json", json);
        Debug.Log(json);
    }

    public void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.json");

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        GameObject.FindGameObjectWithTag("Player").transform.position = saveObject.playerPosition;
    }

    public class SaveObject
    {
        public int goldAmount;
        public Vector3 playerPosition;
        public int chest1choice;
        public bool chest1success;
        public string chest1item;
    }


    private void movePlayer()
    {
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }


    private IEnumerator Attack()
    {
        Debug.Log("Attack");
        if (!isAttacking && !IsMoving)
        {

            isAttacking = true;

            //myanimator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(3); //This is a hardcoded cast time, for debugging

            //Debug.Log("Attack done");

            StopAttack();
        }
    }
    public void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            //myanimator.SetBool("attack", isAttacking);
        }
    }

}
