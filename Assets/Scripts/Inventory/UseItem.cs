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
    [SerializeField]
    PlantManager plantManager;
    public GameObject groundItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            Use();
            Harvest();
        }
    }

    public void Use(){
        currentItem = hotbar.currentItem.item;
        if(currentItem!=null){
            if(inventory.database.GetItem[currentItem.Id].type == ItemType.Plant){
                PlantObject plantObject = inventory.database.GetItem[currentItem.Id] as PlantObject;
                Plant(plantObject);
            }
        }

        
    }

    public void Harvest(){
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        var position = plantable.WorldToCell(worldPoint);

        if(plants.GetTile(position))
        {
            foreach(Plant plant in plantManager.Container){
                if(plant.location.Equals(position)&&plant.harvestable){
                    var obj = Instantiate(groundItemPrefab, position, Quaternion.identity);
                    obj.GetComponent<GroundItem>().item = plant.productItem;
                    plants.SetTile(position, null);
                    plantManager.Container.Remove(plant);
                    break;
                }
            }


        }
    }



    public void Plant(PlantObject plantObject){
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        var position = plantable.WorldToCell(worldPoint);

        var tile = plantable.GetTile(position);

        if(tile && !plants.GetTile(position))
        {
            plants.SetTile(position, plantObject.stages[0]);
            Plant plant = new Plant(position, plantObject.timeBetweenStages, plantObject.stages, plantObject.productItem);
            plantManager.Container.Add(plant);
            hotbar.currentItem.amount--;
            if(hotbar.currentItem.amount==0)
                inventory.ClearSlot(hotbar.previousIndex);
        }

    }
}
