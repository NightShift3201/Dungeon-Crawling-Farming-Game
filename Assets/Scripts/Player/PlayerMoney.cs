using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{

    public int money;
    [SerializeField]
    TextMeshProUGUI display;
    public IntSO MoneySO;
    // Start is called before the first frame update
    void Start()
    {
        display = GameObject.Find("Amount").GetComponent<TextMeshProUGUI>();
        money = MoneySO.Value;
        display.text="$"+money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool BuyItem(int cost){
        if(money>=cost){
            money-=cost;
            UpdateMoney();
            return true;
        }
        return false;

    }

    public void SellItem(int cost){
        money+=cost;
        UpdateMoney();
    }

    public void UpdateMoney(){
        MoneySO.Value = money;
        display.text="$"+money;
    }
}
