using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject DamagePopup;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator swordAnimator;

    Vector2 movement;

    public bool canMove = true;
    bool isDashing = false;


    public float dashTime;
    public float currentDashTime;

    float dashLength = 2f;

    public float health;
    public float maxHealth;

    //public float invincibility;
    //public bool isInvincible;
    public bool isKnockedBack;

    public HealthBar healthBar;
    public FloatSO healthSO;

    void Start(){
        Health = healthSO.Value;
        healthBar.maxHealth(maxHealth);
        healthBar.setHealth(healthSO.Value);
        
    }

    public float Health{
        set{
            health=value;
            healthSO.Value = health;
            healthBar.setHealth(health);
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
                //if(swordAnimator){
                    swordAnimator.SetFloat("LastHorizontal",movement.x);
                    swordAnimator.SetFloat("LastVertical",movement.y);
                //}

            }
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
    public void Knockback(float knockbackForce, Vector3 enemyPosition) {
        Vector2 direction = (transform.position - enemyPosition).normalized;
        StartCoroutine(MoveBack(direction, knockbackForce));
    }

    IEnumerator MoveBack(Vector2 direction, float knockbackForce) {
        isKnockedBack = true;

        float elapsedTime = 0;
        Vector2 startingPos = rb.position;
        while (elapsedTime < 0.1f) {
            float t = elapsedTime / 0.1f;
            rb.MovePosition(Vector2.Lerp(startingPos, startingPos + direction * (knockbackForce+0.5f), t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
    }

    public void playerHurt(float damage){
        Health-=damage;
        var text = Instantiate(DamagePopup, transform.position,Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
        StartCoroutine(FlickerCoroutine());

        //animator.SetTrigger("Hit");
        //cameraAnimator.SetTrigger("Shake");
    }


    void FixedUpdate(){
        if(canMove&&!isDashing&&!isKnockedBack){
            rb.MovePosition(rb.position + movement.normalized *moveSpeed*Time.fixedDeltaTime);
            //rb.AddForce(movement.normalized*moveSpeed*Time.fixedDeltaTime*1000);
        }
    }

    public void Defeated(){
        animator.SetTrigger("Death");
        enabled = false;
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
