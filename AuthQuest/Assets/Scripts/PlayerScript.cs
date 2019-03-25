using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : CharacterScript
{


    

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        
        base.Update();
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
             if (Input.GetKeyDown(KeyCode.Space))
             {
                attackRoutine = StartCoroutine(Attack());
                // StartCoroutine(Attack());
                 
             }
        }

       private IEnumerator Attack()
       {
           if (!isAttacking && ! IsMoving)
           {

            isAttacking = true;

            myanimator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(3); //This is a hardcoded cast time, for debugging

            Debug.Log("Attack done");

            StopAttack();
           }
       }

       private IEnumerator ClickAttack()
       {
           bool LMB = Input.GetMouseButtonDown(0);

           if (LMB) {
               

           }
       }
}
