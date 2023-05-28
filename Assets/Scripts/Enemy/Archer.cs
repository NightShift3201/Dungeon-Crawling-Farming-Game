using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    public GameObject orbPrefab;
    // Start is called before the first frame upda

    public override void FixedUpdate(){
        //MOVEMENT
        float distance = Vector3.Distance(transform.position, GameObject.Find("Player").GetComponent<Transform>().position);
        if (!isKnockedBack &&!attacking&& detectionZone.detectedObj.Count > 0 && health!=0 && distance>4f){
            Move();
        }
        if(distance<4f){
            MoveAway();
        }
        
        if(detectionZone.detectedObj.Count==0){
            animator.SetFloat("Speed", 0);
        }
    }

    public void MoveAway(){
        
    }
    
    public override void Attack(){
        attacking = true; //change later to be set in animation
        var obj = Instantiate(orbPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<BulletController>().enemy = this;
        obj.GetComponent<BulletController>().bulletStrength = 1f;
        Rigidbody2D rb= obj.GetComponent<Rigidbody2D>();
        Vector2 direction = (GameObject.Find("Player").GetComponent<Transform>().position - obj.transform.position).normalized;
        rb.velocity = direction*5f;
        attacking = false;//change later to be set in animation
    }
}
