using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public InventoryObject inventory;
    public PlayerMoney money;
    private int totalValue;
    public TextMeshProUGUI display;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTotalValue();
    }

    public void SellItems(){
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            for (int j = 0; j < inventory.Container.Items[i].amount; j++)
            {
                money.SellItem(inventory.Container.Items[i].ItemObject.value);
            }    

        }
        inventory.Clear();
    }

    public void ChangeTotalValue(){
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            for (int j = 0; j < inventory.Container.Items[i].amount; j++)
            {
                totalValue+=inventory.Container.Items[i].ItemObject.value;
            }    

        }
        display.text = "$"+totalValue;
        totalValue = 0;
    }
}
