using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Food,
    Equipment,
    Default,
    Plant
}

public enum Attributes{
    
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public int Id;
    public ItemType type;
    [TextArea(15,20)]
    public string description; 
}

[System.Serializable]
public class Item{
    public string Name;
    public int Id;
    public Item(ItemObject item){
        Name = item.name;
        Id= item.Id;
    }
}
