using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{

    public GameObject groundItemPrefab;
    public ItemObject[] drops = new ItemObject[0];
    public int[] chance = new int[0];
    //public int[] duplicationChance = new int[0];

    public void DropLoot(){
        int randomNumber = Random.Range(0,100);
        for(int  i= 0; i<drops.Length;i++){
            if(randomNumber<chance[i]){
                var obj = Instantiate(groundItemPrefab, transform.position, Quaternion.identity);
                obj.GetComponent<GroundItem>().item = drops[i];
                return;
            }
        }
    }
}
