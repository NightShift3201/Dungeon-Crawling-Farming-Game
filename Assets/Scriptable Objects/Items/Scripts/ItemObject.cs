using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Food,
    Equipment,
    Default,
    Plant,
    WaterCan
}


public abstract class ItemObject : ScriptableObject
{

    public Sprite uiDisplay;
    public bool stackable;
    public Item data = new Item();
    public ItemType type;
    [TextArea(15,20)]
    public string description; 
    public int value;
}

[System.Serializable]
public class Item{
    public string Name;
    public int Id = -1;
    public Item(){
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item){
        Name = item.name;
        Id= item.data.Id;
    }
}
