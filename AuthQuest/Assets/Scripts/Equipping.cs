using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipping : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeSprite(Sprite newSprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
    
}
