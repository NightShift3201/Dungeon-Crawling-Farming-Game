using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public int attackDamage; 
    public int defenseStat;
    public int speedStat;

    public void Awake(){
        type = ItemType.Equipment;
    }
}
