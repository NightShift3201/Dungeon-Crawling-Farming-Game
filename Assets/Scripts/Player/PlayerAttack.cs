using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCounter;
    public float attackSpeed;
    public Animator animator;
    public Animator swordAnimator;
    public PlayerMovement movement;
    public InventoryObject equipment;
    // Start is called before the first frame update
    void Start()
    {
        //EquipmentObject equipmentObject = equipment.database.GetItem[equipment.Container.Items[6].ID] as EquipmentObject;
        //GameObject obj = Instantiate(equipmentObject.weapon) as GameObject;
        //obj.transform.parent = transform;
        //swordAnimator = obj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCounter<=0f){
            if(Input.GetMouseButtonDown(0)){
                attackCounter = attackSpeed;
                animator.SetTrigger("Attacking");
                swordAnimator.SetTrigger("Attacking");

            }

            movement.canMove = true;
        }
        else{
            attackCounter-=Time.deltaTime;
            movement.canMove=false;
            animator.SetFloat("Speed",0);
        }
    }
}
