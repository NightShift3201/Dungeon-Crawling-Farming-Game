using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlantManager : MonoBehaviour
{
    public PlantContainerObject Container;
    public Tilemap plants;


    // Update is called once per frame
    void Start()
    {
        if(plants)
        foreach(Plant plant in Container.Container){
            plants.SetTile(plant.location, plant.stages[plant.stageIndex-1]);

        }
        
    }

    public void UpdatePlants(){
        foreach(Plant plant in Container.Container){
            plant.timeWatered--;
            if(!plant.harvestable&&plant.timeWatered>0){
                plant.timeToNextStage--;
                if(plant.timeToNextStage==0){
                    if(plant.stages.Count == plant.stageIndex+1){
                        plant.harvestable = true;
                    }
                    plants.SetTile(plant.location, plant.stages[plant.stageIndex]);
                    plant.stageIndex++;
                    plant.timeToNextStage = plant.timeBetweenStages;
                }
            }
            if(plant.timeWatered==0){
                //set the tile back to dry
            }

        }
    }

    public Plant FindPlant(Vector3Int location){
        foreach(Plant plant in Container.Container){
            if(location.Equals(plant.location)){
                return plant;
            }
        }
        return null;
    }


    public void TimePassed(int minutes){
        Debug.Log(minutes);
        foreach(Plant plant in Container.Container){
            if(!plant.harvestable){
                int totalTicks = plant.timeToNextStage - minutes;
                while (totalTicks <= 0)
                {
                    if (plant.stages.Count == plant.stageIndex + 1)
                    {
                        plant.harvestable = true;
                        break;
                    }

                    plants.SetTile(plant.location, plant.stages[plant.stageIndex]);
                    plant.stageIndex++;
                    totalTicks += plant.timeBetweenStages;
                }

                plant.timeToNextStage = totalTicks;


            }

        }
    }
}

[System.Serializable]
public class Plant{
    public bool harvestable;
    public Vector3Int location;
    public int timeBetweenStages;
    public int timeToNextStage;
    public List<Tile> stages = new List<Tile>();
    public int stageIndex;
    public ItemObject productItem;
    public int timeWatered;

    public Plant(Vector3Int location, int timeBetweenStages, List<Tile> stages, ItemObject productItem){
        this.location = location;
        this.timeBetweenStages = timeBetweenStages;
        this.stages = stages;
        this.productItem = productItem;
        timeToNextStage=  timeBetweenStages;
        stageIndex = 1;
        harvestable = false;
        timeWatered=0;
    }
}
