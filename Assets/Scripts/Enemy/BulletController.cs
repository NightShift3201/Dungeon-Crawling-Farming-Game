using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    public Enemy enemy;
    public float bulletStrength;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Wall"){
            Destroy(gameObject);
        }
        if(other.tag == "Player"){
            Destroy(gameObject);
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.playerHurt(enemy.damage);
            player.Knockback(bulletStrength, transform.position);
        }
    }
}
