using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter2D(Collider2D other){
        
        var item = other.GetComponent<GroundItem>();
        if(item){
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item,1)){
                Destroy(other.gameObject);
            }
            

        }


    }
    private void Start(){

    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            inventory.Save();
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            inventory.Load();
        }
        if(Input.GetKeyDown(KeyCode.Backspace)){
            inventory.Clear();
        }
    }


}
