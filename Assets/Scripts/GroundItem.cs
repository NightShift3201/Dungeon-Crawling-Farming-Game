using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public ItemObject item;

    void Start(){
        GetComponent<SpriteRenderer>().sprite = item.uiDisplay;
    }
}
