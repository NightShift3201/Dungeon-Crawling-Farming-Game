using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public DungeonRoomManager RoomManager;
    public GameObject DamagePopup;
    public Animator animator;
    public Rigidbody2D rb;

    public float weight;
    public float moveSpeed;
    public float damage;

    public LootDrop loot;

    public SpriteRenderer spriteRenderer;
    private float flickerTime = 0.1f;
    public int flickerCount = 1;


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
                RoomManager.numberOfSlimes-=1;
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
        RoomManager = GameObject.Find("DungeonRoomManager").GetComponent<DungeonRoomManager>();
        animator = GetComponent<Animator>();
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCooldown<=0f){
            if(attackZone.inRange.Count>0){
                Attack();
                attackCooldown = attackTime;
            }
        }
        else{
            attackCooldown-=Time.deltaTime;
        }
        
        //stops from continouslly hitting
        if(damageCooldown>0f){
            damageCooldown-=Time.deltaTime;
        }
    }

    public virtual void FixedUpdate(){
        //MOVEMENT
        if (!isKnockedBack &&!attacking&& detectionZone.detectedObj.Count > 0 && health!=0){
            Move();
        }
        
        if(detectionZone.detectedObj.Count==0){
            animator.SetFloat("Speed", 0);
        }
    }

    public void Move(){
        Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
        animator.SetFloat("Speed", direction.sqrMagnitude);
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    //KNOCKBACK AFTER PLAYER HIT
    public void Knockback(float knockbackForce) {
        attackCooldown = attackTime; //resets attackcooldown after knockback
        Vector2 direction = (transform.position - GameObject.Find("Player").GetComponent<Transform>().position).normalized;
        StartCoroutine(MoveBack(direction, knockbackForce));
    }

    IEnumerator MoveBack(Vector2 direction, float knockbackForce) {
        isKnockedBack = true;

        float elapsedTime = 0;
        Vector2 startingPos = rb.position;
        while (elapsedTime < 0.1f) {
            float t = elapsedTime / 0.1f;
            rb.MovePosition(Vector2.Lerp(startingPos, startingPos + direction * (knockbackForce - weight + 2), t));
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
    public virtual void Attack(){
        StartCoroutine(AttackMovement());

    }
    private IEnumerator AttackMovement()
    {
        // Disable regular movement
        attacking = true;
        animator.SetTrigger("Attacking");
        float elapsedTime = 0;
        Vector2 startingPos = rb.position;
        Vector2 direction = (GameObject.Find("Player").GetComponent<Transform>().position - transform.position);
        yield return new WaitForSeconds(0.3f);
        if(attackCooldown<=attackTime-0.2f){
            while (elapsedTime < 0.2f && !isKnockedBack && Health>=0) {
                float t = elapsedTime / 0.2f;
                rb.MovePosition(Vector2.Lerp(startingPos, startingPos + direction , t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        // Enable regular movement again
        attacking = false;
    }

    public float damageCooldown;
    float damageTime=0.5f;
    public void OnCollisionEnter2D(Collision2D col){
        if(damageCooldown<=0f){
            if(col.gameObject.tag == "Player"){
                PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
                player.playerHurt(damage);
                player.Knockback(weight, transform.position);
                damageCooldown=damageTime;
                //CameraShake.Instance.ShakeCamera(2,0.5f);
            }
            
        }
    }

    public void TakeDamage(float damage){
        Health-=damage;
        animator.SetTrigger("Hurt");
        var text = Instantiate(DamagePopup, transform.position,Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }


    public void RemoveEnemy(){ //referenced in animation
        loot.DropLoot();
        Destroy(gameObject);
    }
}
