using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 1;
    void Start()
    {
        rb2d = GetComponentInParent<Rigidbody2D> ();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(0, 0);
        if (horizontal > 0)
        {
            moveDirection.x = 1;
        }
        else if (horizontal < 0)
        {
            moveDirection.x = -1;
        }
        else if (vertical > 0)
        {
            moveDirection.y = 1;
        }
        else if (vertical < 0)
        {
            moveDirection.y = -1;
        }
        transform.Translate(moveDirection * speed * Time.deltaTime);
        //rb2d.AddForce(movement * speed);
    }
}
