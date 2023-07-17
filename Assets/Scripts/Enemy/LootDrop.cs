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
                for (int j = 0; j < drops[i].number; j++)
                {
                    var obj = Instantiate(groundItemPrefab, transform.position+new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), Quaternion.identity);
                    obj.GetComponent<GroundItem>().item = drops[i].drop;
                }
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
