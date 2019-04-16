using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject unitGameObject;
    //private IUnit unit;
    public GameObject player;

    private void Awake()
    {
        SaveObject saveObject = new SaveObject
        {
            goldAmount = 5,
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log(loadedSaveObject.goldAmount);
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Save()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        SaveObject saveObject = new SaveObject
        {
            goldAmount = 5,
            playerPosition = playerPosition
        };
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public class SaveObject
    {
        public int goldAmount;
        public Vector3 playerPosition;

    }
}
