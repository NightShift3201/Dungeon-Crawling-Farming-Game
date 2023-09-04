using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseItem : MonoBehaviour
{

    public InventoryObject inventory;
    public DisplayHotbar hotbar;
    InventorySlot currentItem;
    [SerializeField]
    Tilemap plantable;
    [SerializeField]
    Tilemap plants;
    [SerializeField]
    PlantManager plantManager;
    public GameObject groundItemPrefab;

    public Texture2D WateringCanTexture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            currentItem = hotbar.currentItem;
            Use();
            Harvest();
        }
        /*
        if(currentItem != null && currentItem.item.Id>=0&&inventory.database.Items[currentItem.item.Id].type== ItemType.WaterCan){
            Cursor.SetCursor(WateringCanTexture,Vector2.zero, CursorMode.Auto);
        }
        else{
            Cursor.visible = false;
        }
        */
        
    }

    public void Use(){
        if(currentItem != null && currentItem.item.Id>=0){
            if(inventory.database.Items[currentItem.item.Id].type == ItemType.Plant){
                PlantObject plantObject = inventory.database.Items[currentItem.item.Id] as PlantObject;
                Plant(plantObject);
            }
            else if(inventory.database.Items[currentItem.item.Id].type== ItemType.WaterCan){
                Water();
            }
        }

        
    }

    public void Harvest(){
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        var position = plantable.WorldToCell(worldPoint);

        if(plants.GetTile(position))
        {
            foreach(Plant plant in plantManager.Container.Container){
                if(plant.location.Equals(position)&&plant.harvestable){
                    foreach(PlantHarvest harvest in plant.harvests){
                        for (int i = 0; i < Random.Range(harvest.minAmount,harvest.maxAmount+1); i++)
                        {
                            var obj = Instantiate(groundItemPrefab, position+new Vector3(0.5f+0.1f*i,0.5f+0.1f*i,0f), Quaternion.identity);
                            obj.GetComponent<GroundItem>().item = harvest.productItem;
                        }


                    }
                    plants.SetTile(position, null);
                    plantManager.Container.Container.Remove(plant);
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
            Plant plant = new Plant(position, plantObject.timeBetweenStages, plantObject.stages,plantObject.harvests, plantObject.name);
            plantManager.Container.Container.Add(plant);
            hotbar.currentItem.amount--;
            if(hotbar.currentItem.amount==0)
                inventory.Container.Items[hotbar.previousIndex].RemoveItem();
        }

    }

    public void Water(){
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        var position = plantable.WorldToCell(worldPoint);

        var tile = plantable.GetTile(position);

        if(tile && plants.GetTile(position))
        {
            //plantable.SetTile(position, ); set to watered land
            Plant plant = plantManager.FindPlant(position);
            plant.timeWatered = 100;
        }//add level array to itemobject, add level to item
    }
}
