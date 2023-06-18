using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlantManager : MonoBehaviour
{
    public List<Plant> Container = new List<Plant>();
    public Tilemap plants;


    // Update is called once per frame
    void Update()
    {

        
    }

    public void UpdatePlants(){
        foreach(Plant plant in Container){
            if(!plant.harvestable){
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

        }
    }

    public void AddPlant(){

    }
}

[System.Serializable]
public class Plant{
    public bool harvestable;
    public Vector3Int location;
    public float timeBetweenStages;
    public float timeToNextStage;
    public List<Tile> stages = new List<Tile>();
    public int stageIndex;
    public ItemObject productItem;

    public Plant(Vector3Int location, float timeBetweenStages, List<Tile> stages, ItemObject productItem){
        this.location = location;
        this.timeBetweenStages = timeBetweenStages;
        this.stages = stages;
        this.productItem = productItem;
        timeToNextStage=  timeBetweenStages;
        stageIndex = 1;
        harvestable = false;
    }
}