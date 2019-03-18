using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Animator myAnimator;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    public void Move()
        {
            transform.Translate(direction*speed*Time.deltaTime);
        }

        private void GetInput()
        {
            direction = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;  
            }
             if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left; 
            } 
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down; 
            }
             if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right; 
            }
             if (IAnimationClipSource.GetKey(KeyCode.Space)) 
             {
                 StartCoroutine(Attack());
             }
        }

        private IEnumerator Attack()
        {
            myAnimator.SetBool("attack", true);
            yield return new WaitForSeconds(3); // this is a hardcoded cast time f\or debugging


        }
}
