using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour
{
    
      [SerializeField]
    private float speed;

    // a reference to the character's animator
    protected Animator myanimator;


    // The Player's Direction
    protected Vector2 direction;

    private Rigidbody2D myRigidbody;

    protected bool isAttacking = false;

    // protected Coroutine attackRoutine;
    public bool IsMoving
    {
        get {
            return direction.x != 0 || direction.y != 0;
        }
    }
    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
    }

    // Update is marked as virtual, so that we can override it in the subclasses
    protected virtual void Update()
    {
        
        HandleLayers();
    
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;

    
    }

    public void HandleLayers()
    {
        // Checks if we are moving or standing still, if we are moving then we need to play the mov.
        if(IsMoving)
        {
        // Animate's the player's movement
        ActivateLayer("WalkLayer");

        // Sets the animation parameter so that he faces the correct direction
        myanimator.SetFloat("x", direction.x);
        myanimator.SetFloat("y", direction.y);

        StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            // Makes sure that we will go back to idle when we aren't pressing any keys.
            ActivateLayer("IdleLayer");
        }
        
    }
    

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myanimator.layerCount; i++)
        {
            myanimator.SetLayerWeight(i, 0);
        }

        myanimator.SetLayerWeight(myanimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        //if (attackRoutine != null)
        //{
            // StopCoroutine(attackRoutine);
            isAttacking = false;
            myanimator.SetBool("attack", isAttacking);
        //}
    }
}

    