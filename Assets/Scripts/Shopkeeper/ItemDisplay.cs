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
        if(canCraftItem()&&money.BuyItem(item.price)){
            playerInventory.AddItem(new Item(item.product),1);
            CraftItem();
        }

    }

    public bool canCraftItem(){
        int count = 0;
        for (int i = 0; i < item.neededItems.Count; i++)
        {
            for(int j =0; j<playerInventory.Container.Items.Length;j++){
                if(playerInventory.Container.Items[i].item.Id==item.neededItems[i].item.data.Id){
                    if(item.neededItems[i].amount <= playerInventory.Container.Items[i].amount){
                        count++;
                        break;
                    }
                }
            }
            
        }
        return (count==item.neededItems.Count);
        
    }

    public void CraftItem(){
        for (int i = 0; i < item.neededItems.Count; i++)
        {
            for(int j =0; j<playerInventory.Container.Items.Length;j++){
                InventorySlot slot = playerInventory.Container.Items[i];
                if(slot.item.Id==item.neededItems[i].item.data.Id){
                    slot.amount-=item.neededItems[i].amount;
                    if(slot.amount==0)
                        slot.RemoveItem();
                    break;
                }
            }
            
        }
    }
}
