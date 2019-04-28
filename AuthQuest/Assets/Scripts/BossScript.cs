using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject levelmanager;
    // Start is called before the first frame update
    void Start()
    {
        levelmanager = GameObject.FindWithTag("levelM");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        Debug.Log("boss was killed");
        levelmanager.GetComponent<LevelGenerator>().ClearPathToChest();
    }
}
