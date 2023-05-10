using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomManager : MonoBehaviour
{

    public Vector2 minPos;
    public Vector2 maxPos;
    public GameObject Slime;
    public float numberOfSlimes = Random.Range(1,3);
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<numberOfSlimes;i++){
            Vector2 position = new Vector2(Random.Range(minPos.x, maxPos.y), Random.Range(minPos.y, maxPos.y));
            //create check to see if position is tile that exists on default
            Instantiate(Slime, position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfSlimes==0){
            openDoors();
        }
    }
    public void openDoors(){
        //open doors at end of level
        //remove from wall
        //add open to default
    }
}
