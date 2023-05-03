using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator swordAnimator;

    Vector2 movement;

    public float attackCounter;
    public float attackSpeed;
    public bool canMove = true;


    // Update is called once per frame
    void Update()
    {
        if(canMove){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
            animator.SetFloat("Speed",movement.sqrMagnitude);

            if(movement.x != 0 || movement.y !=0){
                animator.SetFloat("LastHorizontal",movement.x);
                animator.SetFloat("LastVertical",movement.y);
                swordAnimator.SetFloat("LastHorizontal",movement.x);
                swordAnimator.SetFloat("LastVertical",movement.y);
            }
        }
    

        if(attackCounter<=0f){
            if(Input.GetMouseButtonDown(0)){
                attackCounter = attackSpeed;
                animator.SetTrigger("Attacking");
                swordAnimator.SetTrigger("Attacking");
            }

        }
        else{
            attackCounter-=Time.deltaTime;
        }
    }

    void FixedUpdate(){
        if(canMove)
            rb.MovePosition(rb.position + movement.normalized *moveSpeed*Time.fixedDeltaTime);
    }

    public void LockMovement(){
        canMove=false;
    }

    public void UnlockMovement(){
        canMove=true;
    }

}
