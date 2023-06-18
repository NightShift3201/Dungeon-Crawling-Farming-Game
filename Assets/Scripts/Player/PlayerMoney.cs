using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{

    public int money;
    [SerializeField]
    TextMeshProUGUI display;
    // Start is called before the first frame update
    void Start()
    {
        display.text="$"+money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem(int cost){
        money-=cost;
        UpdateMoney();
    }

    public void SellItem(int cost){
        money+=cost;
        UpdateMoney();
    }

    public void UpdateMoney(){
        display.text="$"+money;
    }
}
