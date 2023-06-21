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

    public List<RequiredItem> neededItems = new List<RequiredItem>();
    
}

[System.Serializable]
public class RequiredItem{
    public ItemObject item;
    public int amount;
}


