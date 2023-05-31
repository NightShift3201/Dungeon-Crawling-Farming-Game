using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Plant Object", menuName = "Inventory System/Items/Plants")]
public class PlantObject : ItemObject
{
    
    public float growthTime;
    public List<Tile> stages = new List<Tile>();
    
    public void Awake(){
        type = ItemType.Plant;
    }

}
