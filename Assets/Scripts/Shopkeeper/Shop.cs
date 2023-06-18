using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class ItemsForSale{
    public int price;
    public ItemObject product;
    
}
[System.Serializable]
public class CraftableItems{
    public int price;
    public ItemObject product;
    public List<ItemObject> neededItems = new List<ItemObject>();
}
