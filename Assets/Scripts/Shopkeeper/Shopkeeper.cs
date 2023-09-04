using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shopkeeper : MonoBehaviour
{
    public List<ItemsForSale> sellableItems = new List<ItemsForSale>();
    public List<ItemsForSale> craftableItems = new List<ItemsForSale>();

    public int shopLevel;
    public GameObject shopPanel;

    public DisplayShop display;
    private GameObject hotbar;
    public GameObject inventoryPanel;
    private GameManager GameManager;


    void Awake(){
        hotbar = GameObject.Find("Hotbar");
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OpenShopPanel(){
        if(!shopPanel.activeSelf){
            shopPanel.SetActive(true);
            display.CreateSlots(sellableItems, shopLevel);
            hotbar.SetActive(false);
            inventoryPanel.SetActive(true);
            GameManager.canOpenInventory = false;
        }
        else{
            ClosePanel();
        }

    }
    void ClosePanel(){
        shopPanel.SetActive(false);
        display.ClearSlots();
        hotbar.SetActive(true);
        inventoryPanel.SetActive(false);
        GameManager.canOpenInventory = true;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)&&shopPanel.activeSelf){
            ClosePanel();

        }
        if(Input.GetKeyDown(KeyCode.E) && shopPanel.activeSelf){
            shopPanel.SetActive(false);
            display.ClearSlots();
            GameManager.canOpenInventory = true;
        }
    }

    void OnMouseDown(){
        OpenShopPanel();
    }

}
