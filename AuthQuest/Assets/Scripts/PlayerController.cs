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
    public bool canMove;
    private bool invincible;
    protected Coroutine attackRoutine;
    public SpriteRenderer playerSpriteRenderer;

    public bool isSprint;
    private KeyCode LastKey;
    private float timeSinceKeyPressLast;
    private KeyCode ThisKey;
    private float timeSinceKeyPressThis;
    public double keyDelay;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;

    public Image customImage; //get this sword out of here. not the right place for it
    public Image sword;

    public bool isOpened;
    public bool moveme;
    public Vector3 moveto;

    public List<int> chestNumber = new List<int>();
    public List<int> choice = new List<int>();
    public List<bool> success = new List<bool>();
    public List<string> itemReceived = new List<string>();

    public GameObject meleeWeapon;
    public GameObject rangeWeapon;

    public GameObject attackIndicator;

    public GameObject HUD;
    public GameObject CoolBar1;
    public int cool1;
    public GameObject CoolBar2;
    public int cool2;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Player Awake");
        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.Log("destroy player");
            Destroy(gameObject);
        }
        moveme = false;
        DontDestroyOnLoad(gameObject);
        isOpened = false;
        isAttacking = false;

        moveSpeed = 10 + (GetComponent<PlayerStats>().Speed * 2);//PlayerStats = GetComponent<CharacterStat>(); 
        keyDelay = .2; //PlayerStats = GetComponent<CharacterStat>();
        isSprint = false;
        timeSinceKeyPressLast = -1;
        timeSinceKeyPressThis = 0;
        playerSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        invincible = false;

        attackIndicator = GameObject.FindGameObjectWithTag("attack");

        HUD = GameObject.Find("HUD");
        CoolBar1 = GameObject.Find("CoolDownBar1");
        CoolBar2 = GameObject.Find("CoolDownBar2");
        cool1 = 0;
        cool2 = 0;
    }

    // Update is called once per frame
    void Update() //characer movment
    {
        coolDown();
        if (moveme)
        {
            transform.position = moveto;
            moveme = false;
        }

        moveSpeed = 10 + (GetComponent<PlayerStats>().Speed * 2);
        SprintCheck();
        if (canMove)
        {
            movePlayer();
        }


        if (Input.GetMouseButtonDown(0) && cool1 <= 0)
        {
            CoolBar1.GetComponent<CoolDown>().HealthRegenerate(1);
            cool1 = meleeWeapon.GetComponent<WeaponStats>().weaponSpeed;
            attackRoutine = StartCoroutine(Attack());
            // StartCoroutine(Attack());

        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void movePlayer()
    {

        float speed;

        if (isSprint)

        {
            speed = moveSpeed * 2;
        }
        else
        {
            speed = moveSpeed;
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
        float speed;

        if (isSprint)
        {
            speed = moveSpeed * 2;
        }
        else
        {
            speed = moveSpeed;
        }

        float moveH = Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.8f);
        float moveV = Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 0.8f);
        theRB.velocity = new Vector2(moveH, moveV);

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
    private void SprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            timeSinceKeyPressLast = timeSinceKeyPressThis;
            LastKey = ThisKey;
            timeSinceKeyPressThis = Time.time;
            ThisKey = KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            timeSinceKeyPressLast = timeSinceKeyPressThis;
            LastKey = ThisKey;
            timeSinceKeyPressThis = Time.time;
            ThisKey = KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            timeSinceKeyPressLast = timeSinceKeyPressThis;
            LastKey = ThisKey;
            timeSinceKeyPressThis = Time.time;
            ThisKey = KeyCode.D;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            timeSinceKeyPressLast = timeSinceKeyPressThis;
            LastKey = ThisKey;
            timeSinceKeyPressThis = Time.time;
            ThisKey = KeyCode.S;
        }

        if (LastKey == ThisKey && keyDelay > (timeSinceKeyPressThis - timeSinceKeyPressLast))
        {
            isSprint = true;
        }
        else
        {
            isSprint = false;
        }

    }

    public void EquipWeapon(GameObject weaponToEquip)
    {
        Debug.Log("equipped weapon");
        WeaponStats weaponstats = weaponToEquip.GetComponent<WeaponStats>();

        HUD.GetComponent<HUDScript>().ChangeWeapon(weaponToEquip);
        if (weaponstats.isRange)
        {
            //if(rangeWeapon) 
            {
                Debug.Log("drop ranged weapon");
                Vector3 position = this.transform.position + new Vector3(5, 5, 0);
                rangeWeapon.transform.position = position;
                //Instantiate(rangeWeapon, position, Quaternion.identity);
                rangeWeapon.gameObject.SetActive(true);
            }
            rangeWeapon = weaponToEquip;
        }
        else
        {
            //if (meleeWeapon)
            {
                Debug.Log("drop melee weapon");
                Vector3 position = this.transform.position + new Vector3(5, 5, 0);
                meleeWeapon.transform.position = position;
                //Instantiate(meleeWeapon, position, Quaternion.identity);
                meleeWeapon.gameObject.SetActive(true);
            }
            meleeWeapon = weaponToEquip;
            //Equipping equ = attackIndicator.GetComponent<Equipping>();
            //equ.ChangeSprite(meleeWeapon.GetComponent<SpriteRenderer>().sprite);
            Sprite newSprite = meleeWeapon.GetComponent<SpriteRenderer>().sprite;
            attackIndicator.GetComponent<Equipping>().ChangeSprite(newSprite);
            attackIndicator.transform.localRotation = meleeWeapon.GetComponent<WeaponStats>().rotation;
            attackIndicator.transform.localScale = meleeWeapon.GetComponent<WeaponStats>().scale / 1.5F;

            Debug.Log(meleeWeapon.GetComponent<WeaponStats>().rotation);
            Debug.Log(attackIndicator.transform.eulerAngles);
            Debug.Log("sprite should change");
        }

        weaponToEquip.gameObject.SetActive(false);

    }

    public void MovePlayer(Vector3 moveto2){
        //Debug.Log("set to location");
        moveme = true;
        moveto = moveto2;
    }

    public void coolDown()
    {
        if(cool1 > 0)
        {
            cool1--;
        }
        if (cool2 > 0)
        {
            cool2--;
        }

        float math1 = .005f;
        float math2 = .005f;
        CoolBar1.GetComponent<CoolDown>().TakeDamage(math1);
        CoolBar2.GetComponent<CoolDown>().TakeDamage(math2);
    }
    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Virus1" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            if (!invincible)
            {
                invincible = true;
                StartCoroutine(FlashInvisible());
            }
        }

    }
    IEnumerator FlashInvisible()
    {
        //Debug.Log("flash");
        for (int i = 0; i < 5; i++)
        {
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
            Debug.Log("flash");
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
        }
        playerSpriteRenderer.enabled = true;
        invincible = false;
    }

    public bool CheckIfInvincible()
    {
        return invincible;
    }

}
