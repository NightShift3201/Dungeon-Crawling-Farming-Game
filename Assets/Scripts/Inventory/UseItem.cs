using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseItem : MonoBehaviour
{

    public InventoryObject inventory;
    public DisplayHotbar hotbar;
    Item currentItem;
    [SerializeField]
    Tilemap plantable;
    [SerializeField]
    Tilemap plants;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            Use();
        }
    }

    public void Use(){
        currentItem = hotbar.currentItem.item;
        if(inventory.database.GetItem[currentItem.Id].type == ItemType.Plant){
            PlantObject plantObject = inventory.database.GetItem[currentItem.Id] as PlantObject;
            Plant(plantObject.stages[0]);
            hotbar.currentItem.amount--;
        }

    }

    public void Plant(Tile type){
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        var position = plantable.WorldToCell(worldPoint);

        var tile = plantable.GetTile(position);

        if(tile)
        {
            plants.SetTile(position, type);
        }
    }
}
