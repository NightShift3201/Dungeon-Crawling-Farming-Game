using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPlant : MonoBehaviour
{

    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    Tilemap plants;
    [SerializeField]
    Tile test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Plant(Tile type){
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
            var position = tilemap.WorldToCell(worldPoint);

            var tile = tilemap.GetTile(position);

            if(tile)
            {
                plants.SetTile(position, type);
            }
        }
    }
}
