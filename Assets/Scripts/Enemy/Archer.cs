using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    public GameObject orbPrefab;
    bool movingAway = false;
    // Start is called before the first frame upda

    public override void FixedUpdate(){
        //MOVEMENT
        float distance = Vector3.Distance(transform.position, GameObject.Find("Player").GetComponent<Transform>().position);
        if(!isKnockedBack &&!attacking&& detectionZone.detectedObj.Count > 0 && health!=0){
            if (distance < 2f)
            {
                MoveAway();
            }
            else if (distance > 4f)
            {
                movingAway = false;
                Move();
                Debug.Log(movingAway);
            }

            if (movingAway)
            {
                MoveAway();
            }
        }
        
        if(detectionZone.detectedObj.Count==0){
            animator.SetFloat("Speed", 0);
        }
    }

    public void MoveAway(){
        movingAway = true;
        Vector2 direction = (transform.position-detectionZone.detectedObj[0].transform.position).normalized;
        animator.SetFloat("Speed", direction.sqrMagnitude);
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime*3f);
    }
    
    public override void Attack(){
        animator.SetTrigger("Attacking");
        var obj = Instantiate(orbPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<BulletController>().enemy = this;
        obj.GetComponent<BulletController>().bulletStrength = 0.5f;
        Rigidbody2D rb= obj.GetComponent<Rigidbody2D>();
        Vector2 direction = (GameObject.Find("Player").GetComponent<Transform>().position - obj.transform.position).normalized;
        rb.velocity = direction*5f;
    }

    public void setAttack(){
        attacking = !attacking;
    }
}
