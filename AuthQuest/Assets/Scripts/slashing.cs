using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashing : MonoBehaviour
{
    public bool isAttacking;
public CoolDown isCoolin;
   
    protected Coroutine attackRoutine;
    public Animator myanimator;

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
        
        if (isCoolin.isCoolingDown) {

        } 
        if (!isCoolin.isCoolingDown) {
        if (Input.GetMouseButtonDown(0))
        {
            attackRoutine = StartCoroutine(Attack());
            // StartCoroutine(Attack());

        }
    }
    }

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

    private void OnTriggerStay2D(Collider2D other)
    {

        EnemyController ec = other.gameObject.GetComponent<EnemyController>();
        if (ec)
        {
            ec.GemTouchedVirus(this.gameObject);
        }
        else
        {
            EnemyController2 ec2 = other.gameObject.GetComponent<EnemyController2>();
            ec2.GemTouchedVirus(this.gameObject);
        }

    }

}
