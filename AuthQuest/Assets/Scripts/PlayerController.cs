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

    public List<int> chestNumber = new List<int>();
    public List<int> choice = new List<int>();
    public List<bool> success = new List<bool>();
    public List<string> itemReceived = new List<string>();

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
        checkSave();

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

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        for(int i=0; i < chestNumber.Count; i++)
        {
            save.chestNumber.Add(chestNumber[i]);
            save.choice.Add(choice[i]);
            save.success.Add(success[i]);
            save.itemReceived.Add(itemReceived[i]);
        }

        return save;
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.json");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game Saved");
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

    private void checkSave()
    {
        if (Input.GetKeyDown("escape"))
        {
            SaveGame();
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
