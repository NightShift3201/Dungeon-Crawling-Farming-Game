using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage;
    public float knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(damage);
                enemy.Knockback(knockbackForce);
                StartCoroutine(enemy.FlickerCoroutine());
            }
        }
    }
}
