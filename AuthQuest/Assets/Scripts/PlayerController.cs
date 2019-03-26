using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;

    public Image customImage;
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

        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        if(instance.transform.position.x <= -3 
            && instance.transform.position.x >= -5 
            && instance.transform.position.y >= 4 
            && instance.transform.position.y <= 6)
        {

            if(isOpened == false)
            {
                customImage.enabled = true;
            } else
            {
                customImage.enabled = false;
            }
            
            
            if(Input.GetMouseButtonDown(0))
            {
                if(Input.mousePosition.x >= 218 && Input.mousePosition.x <= 378
                    && Input.mousePosition.y >= 37 && Input.mousePosition.y <= 135)
                {
                    Debug.Log("You have successfully cracked the code. You received the sword!");
                    chestNumber.Add(1);
                    choice.Add(1);
                    success.Add(true);
                    itemReceived.Add("Armadyl Godsword");
                    isOpened = true;
                    sword.enabled = true;
                } else if(Input.mousePosition.x >= 442 && Input.mousePosition.x <= 603
                    && Input.mousePosition.y >= 37 && Input.mousePosition.y <= 135)
                {
                    Debug.Log("The chest has been broken. You did not receive the sword!");
                    chestNumber.Add(1);
                    choice.Add(2);
                    success.Add(false);
                    itemReceived.Add("Nothing");
                    isOpened = true;
                } else
                {
                    Debug.Log("Nothing interesting happens");
                }
              
            }

        } else
        {
            customImage.enabled = false;
        }

        if(Input.GetKeyDown("space"))
        {
            SaveGame();
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


}
