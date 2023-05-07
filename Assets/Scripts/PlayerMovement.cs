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
    bool isDashing = false;


    public float dashTime;
    public float currentDashTime;

    float dashLength = 2f;

    public float health;
    public float maxHealth;

    public float invincibility;
    public bool isInvincible;

    void Start(){
        health = maxHealth;
    }

    public float Health{
        set{
            health=value;

            if(health<=0){
                Defeated();
            }
        }
        get{
            return health;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(invincibility<=0f){
            isInvincible = false;
        }
        else{
            invincibility-=Time.deltaTime;
            isInvincible = true;
        }
        //GET MOVEMENT INPUT/ANIMATION

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

        if(canMove){
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
    
        //ATTACK
        if(attackCounter<=0f){
            if(Input.GetMouseButtonDown(0)){
                attackCounter = attackSpeed;
                animator.SetTrigger("Attacking");
                swordAnimator.SetTrigger("Attacking");

            }

            canMove = true;
        }
        else{
            attackCounter-=Time.deltaTime;
            canMove=false;
            animator.SetFloat("Speed",0);
        }


        //DASH
        if(currentDashTime<=0f){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartCoroutine(Dash());
                currentDashTime = dashTime;
            }
        }
        else{
            currentDashTime-=Time.deltaTime;
        }

    }
    private IEnumerator Dash()
    {
        // Disable regular movement
        isDashing = true;

        float elapsedTime = 0;
        Vector2 startingPos = rb.position;
        while (elapsedTime < 0.1f) {
            float t = elapsedTime / 0.1f;
            rb.MovePosition(Vector2.Lerp(startingPos, startingPos + movement * dashLength, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Enable regular movement again
        isDashing = false;
    }

    public void playerHurt(float damage){
        if(!isInvincible){
            Health-=damage;
            StartCoroutine(FlickerCoroutine());
        }

        //animator.SetTrigger("Hit");
        //cameraAnimator.SetTrigger("Shake");
    }

    public void knockbackPlayer(float knockback,Vector2 enemyDirection){

    }

    void FixedUpdate(){
        if(canMove&&!isDashing){
            rb.MovePosition(rb.position + movement.normalized *moveSpeed*Time.fixedDeltaTime);
            //rb.AddForce(movement.normalized*moveSpeed*Time.fixedDeltaTime*1000);
        }
    }

    public void Defeated(){

    }

    public SpriteRenderer spriteRenderer;
    public float flickerTime = 0.1f;
    public int flickerCount = 1;
    public IEnumerator FlickerCoroutine()
    {
        
        for (int i = 0; i < flickerCount; i++)
        {
            if(spriteRenderer!=null){
            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(flickerTime);

            spriteRenderer.color = Color.clear;

            yield return new WaitForSeconds(flickerTime);
            }
        }
        spriteRenderer.color = Color.white;
    }


}
