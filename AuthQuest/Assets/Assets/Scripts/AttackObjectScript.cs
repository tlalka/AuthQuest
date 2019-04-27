using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectScript : CharacterScript
{
    // Start is called before the first frame update
    // void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    protected override void Update()
    {
        faceMouse();

        base.Update();
    }

            void faceMouse()
        {
            Vector3 mouseposition = Input.mousePosition;
            mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);

            Vector2 direction = new Vector2 (
                mouseposition.x - transform.position.x,
                mouseposition.y - transform.position.y
            );

            transform.up = direction;
        }

    
}
