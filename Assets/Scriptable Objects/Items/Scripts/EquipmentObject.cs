using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
    Sword,
    Helmet,
    Chestplate,
    Greaves,
    Boots,
    Runes
}

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public EquipmentType equipment;
    public int attackDamage; 
    public int defenseStat;
    public int speedStat;
    //public GameObject weapon;

    public void Awake(){
        type = ItemType.Equipment;
    }
}
