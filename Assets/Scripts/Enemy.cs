using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    Animator animator;
    public Rigidbody2D rb;

    public float weight;
    public float moveSpeed;

    public SpriteRenderer spriteRenderer;
    public float flickerTime = 0.05f;
    public int flickerCount = 3;

    public PlayerDetection detectionZone;

    public bool isKnockedBack = false;

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

    }

    void FixedUpdate(){
        //MOVEMENT
        if (!isKnockedBack && detectionZone.detectedObj.Count > 0 && health!=0){
            Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
            animator.SetFloat("Speed", direction.sqrMagnitude);
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        if(detectionZone.detectedObj.Count==0){
            animator.SetFloat("Speed", 0);
        }
    }

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
    public IEnumerator FlickerCoroutine()
    {
        
        for (int i = 0; i < flickerCount; i++)
        {

            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(flickerTime);

            spriteRenderer.color = Color.clear;

            yield return new WaitForSeconds(flickerTime);
        }
        spriteRenderer.color = Color.white;
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
