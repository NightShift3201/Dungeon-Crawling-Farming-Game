using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shopkeeper : MonoBehaviour
{
    public List<ItemsForSale> inventory = new List<ItemsForSale>();

    public Inventory playerInventory;

    public GameObject shopPanel;

    public DisplayShop display;


    public void OpenShopPanel(){
        if(!shopPanel.activeSelf){
            shopPanel.SetActive(true);
            display.CreateSlots(inventory);
        }
        else{
            shopPanel.SetActive(false);
            display.ClearSlots();
        }

    }

    void OnMouseDown(){
        OpenShopPanel();
    }

}
