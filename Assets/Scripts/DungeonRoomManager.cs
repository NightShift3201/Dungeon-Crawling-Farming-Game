using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoomManager : MonoBehaviour
{

    public Vector2 minPos;
    public Vector2 maxPos;
    public GameObject Slime;

    public int minSlimes;
    public int maxSlimes;
    public int numberOfSlimes;



    public Tilemap background;
    public Tilemap walls;

    public List<Tile> doorTiles = new List<Tile>();
    public List<Vector3Int> doorTileLocation = new List<Vector3Int>();

    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        numberOfSlimes = Random.Range(minSlimes, maxSlimes +1);
        for(int i = 0; i<numberOfSlimes;i++){
            Vector2 position = new Vector2(Random.Range(minPos.x, maxPos.y), Random.Range(minPos.y, maxPos.y));
            //create check to see if position is tile that exists on default
            Instantiate(Slime, position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfSlimes<=0){
            openDoors();
        }
    }
    public void openDoors(){
        for(int i =0;i<doorTiles.Count;i++){
            walls.SetTile(doorTileLocation[i], null);
            background.SetTile(doorTileLocation[i], doorTiles[i]);
        }
        //open doors at end of level
        //remove from wall
        //add open to default
    }

    public void deathScreen(){

    }
}
