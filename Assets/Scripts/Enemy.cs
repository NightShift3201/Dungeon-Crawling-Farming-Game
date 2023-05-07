using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    Animator animator;
    public Rigidbody2D rb;

    public float weight;
    public float moveSpeed;
    public float damage;



    public SpriteRenderer spriteRenderer;
    public float flickerTime = 0.05f;
    public int flickerCount = 3;


    public PlayerDetection detectionZone;
    public EnemyAttackZone attackZone;
    public float attackCooldown;
    public float attackTime;

    public bool isKnockedBack = false;

    public bool attacking = false;

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
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCooldown<=0f){
            if(attackZone.inRange.Count>0){
                StartCoroutine(Attack());
                attackCooldown = attackTime;
            }
        }
        else{
            attackCooldown-=Time.deltaTime;
        }
    }

    void FixedUpdate(){
        //MOVEMENT
        if (!isKnockedBack &&!attacking&& detectionZone.detectedObj.Count > 0 && health!=0){
            Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
            animator.SetFloat("Speed", direction.sqrMagnitude);
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        
        if(detectionZone.detectedObj.Count==0){
            animator.SetFloat("Speed", 0);
        }
    }
    //KNOCKBACK AFTER PLAYER HIT
    public void Knockback(float knockbackForce) {
        Vector2 direction = (transform.position - GameObject.Find("Player").GetComponent<Transform>().position).normalized;
        StartCoroutine(MoveBack(direction, knockbackForce));
    }

    IEnumerator MoveBack(Vector2 direction, float knockbackForce) {
        isKnockedBack = true;

        float elapsedTime = 0;
        Vector2 startingPos = rb.position;
        while (elapsedTime < 0.1f) {
            float t = elapsedTime / 0.1f;
            rb.MovePosition(Vector2.Lerp(startingPos, startingPos + direction * (knockbackForce - weight + 1), t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
    }


    //ENEMY HIT FLICKER
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

    //ENEMY DAMAGE
    private IEnumerator Attack()
    {
        // Disable regular movement
        attacking = true;
        animator.SetTrigger("Attacking");
        float elapsedTime = 0;
        Vector2 startingPos = rb.position;
        Vector2 direction = (GameObject.Find("Player").GetComponent<Transform>().position - transform.position);
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < 0.3f && !isKnockedBack) {
            float t = elapsedTime / 0.3f;
            rb.MovePosition(Vector2.Lerp(startingPos, startingPos + direction , t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Enable regular movement again
        attacking = false;
    }
    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<PlayerMovement>().playerHurt(damage);
            col.gameObject.GetComponent<PlayerMovement>().knockbackPlayer(weight, transform.position);
        }
    }

    public void TakeDamage(float damage){
        Health-=damage;
        animator.SetTrigger("Hurt");
    }

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }
}
