using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Plant Object", menuName = "Inventory System/Items/Plants")]
public class PlantObject : ItemObject
{
    
    public int timeBetweenStages;
    public List<Tile> stages = new List<Tile>();
    public List<PlantHarvest> harvests;
    public int deathChance;
    public void Awake(){
        type = ItemType.Plant;
    }

}

public class PlantHarvest{
    public ItemObject productItem;
    public int minAmount;
    public int maxAmount;
}