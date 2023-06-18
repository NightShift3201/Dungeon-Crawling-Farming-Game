using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{

    public GameObject groundItemPrefab;
    public Drops[] drops = new Drops[0];


    public void DropLoot(){
        int randomNumber = Random.Range(0,100);
        for(int  i= 0; i<drops.Length;i++){
            if(randomNumber<drops[i].chance){
                var obj = Instantiate(groundItemPrefab, transform.position, Quaternion.identity);
                obj.GetComponent<GroundItem>().item = drops[i].drop;
                return;
            }
        }
    }
}

[System.Serializable]
public class Drops{
    public ItemObject drop;
    public int chance;
    public int number;
}
