using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public PlayerMoney money;
    public InventoryObject playerInventory;
    public ItemsForSale item;
    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("Player").GetComponent<PlayerMoney>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem(){
        money.BuyItem(item.price);
        playerInventory.AddItem(new Item(item.product),1);
    }
}
