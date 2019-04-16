using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class slashing : MonoBehaviour
{
    public bool isAttacking;
    protected Coroutine attackRoutine;
    public Animator myanimator;

    public int weaponSpeed;
    private float cooldownTime;
    private bool isCooldown;

    private float coolingdownCounter = 1;

    private bool coolingdown = false;

   
    // Start is called before the first frame update

        void Start()
    {
        isAttacking = false;
        myanimator.SetBool("slashing", isAttacking);
        myanimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //!!!!!!get rid of this, make player call slashing
            if (Input.GetMouseButtonDown(0))
        {
            attackRoutine = StartCoroutine(Attack());
            // StartCoroutine(Attack());
        }
        
    }
    /*
    public IEnumerator Cooldown() {
        GameObject weapon;
        int speed = weapon.GetComponent<WeaponStats>().weaponSpeed;
        if (speed > speed * 3) {
            StopAttack();
            
            
            
        } else {
            Attack();
        }
    }
    */
   

    private IEnumerator Attack()
    {
        Debug.Log("Attack");
        if (!isAttacking)
        {

            isAttacking = true;
        Debug.Log("attacked");

            myanimator.SetBool("slashing", isAttacking);

            yield return new WaitForSeconds(1); //This is a hardcoded cast time, for debugging

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
            myanimator.SetBool("slashing", isAttacking);
        }
    }

    


}
